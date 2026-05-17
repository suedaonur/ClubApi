using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics.Tensors;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class RegulationChunk
{
    public string Text { get; set; } = string.Empty;
    public ReadOnlyMemory<float> Embedding { get; set; }
}

public class RegulationRagService : IRegulationRagService
{
    private readonly List<RegulationChunk> _chunks = new();
    private readonly string _apiKey;
    private readonly string _filePath;
    private readonly HttpClient _httpClient;

    public RegulationRagService(IConfiguration configuration)
    {
        _apiKey = configuration["Gemini:ApiKey"] ?? string.Empty;
        _filePath = Path.Combine(AppContext.BaseDirectory, "pdf_extracted.txt");
        _httpClient = new HttpClient();
    }

    public async Task InitializeAsync()
    {
        if (_chunks.Count > 0) return; // Zaten yüklendi
        if (string.IsNullOrEmpty(_apiKey)) return; // API key yoksa yükleme yapamayız
        if (!File.Exists(_filePath))
        {
            Console.WriteLine($"[RegulationRagService] HATA: Dosya bulunamadı -> {_filePath}");
            return;
        }

        string fullText = await File.ReadAllTextAsync(_filePath);

        // Basit bir Regex ile "Madde X" üzerinden metni parçalıyoruz (Chunking)
        var pattern = @"(Madde\s+\d+[\s\S]*?)(?=(Madde\s+\d+)|$)";
        var matches = Regex.Matches(fullText, pattern);

        Console.WriteLine($"[RegulationRagService] {matches.Count} parça bulundu, Gemini ile vektörize ediliyor...");

        var tasks = new List<Task>();
        
        // Çoklu istek sınırına (rate limit) takılmamak için biraz aralık verebiliriz ama metin az olduğu için direkt gönderiyoruz
        foreach (Match match in matches)
        {
            var chunkText = match.Value.Trim();
            if (string.IsNullOrWhiteSpace(chunkText)) continue;

            tasks.Add(ProcessChunkAsync(chunkText));
        }

        await Task.WhenAll(tasks);
        Console.WriteLine($"[RegulationRagService] Başarıyla {_chunks.Count} adet parça (chunk) belleğe yüklendi ve vektörize edildi.");
    }

    private async Task ProcessChunkAsync(string text)
    {
        var embedding = await GenerateEmbeddingAsync(text);
        if (embedding == null) return;
        
        lock (_chunks)
        {
            _chunks.Add(new RegulationChunk
            {
                Text = text,
                Embedding = embedding
            });
        }
    }

    private async Task<float[]> GenerateEmbeddingAsync(string text)
    {
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-embedding-001:embedContent?key={_apiKey}";
        var requestBody = new
        {
            model = "models/gemini-embedding-001",
            content = new
            {
                parts = new[] { new { text = text } }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(url, requestBody);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[Gemini Embedding Error] {error}");
            return null;
        }

        using var jsonDoc = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        var values = jsonDoc.RootElement
            .GetProperty("embedding")
            .GetProperty("values")
            .EnumerateArray()
            .Select(x => x.GetSingle())
            .ToArray();

        return values;
    }

    public async Task<string> AskQuestionAsync(string question, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(_apiKey)) return "Gemini API Key eksik. Lütfen appsettings.json'da Gemini:ApiKey değerini ayarlayın.";
        if (_chunks.Count == 0) return "Veri yüklenemedi veya henüz hazır değil. (API Key eksik olabilir veya pdf_extracted.txt bulunamamış olabilir)";

        var questionEmbedding = await GenerateEmbeddingAsync(question);
        if (questionEmbedding == null) return "Sorunuz vektörize edilemedi. Lütfen API anahtarınızı kontrol edin.";

        // .NET 9 TensorPrimitives kullanarak en benzer 3 parçayı buluyoruz (Cosine Similarity)
        var topChunks = _chunks
            .Select(c => new 
            { 
                Chunk = c, 
                Similarity = TensorPrimitives.CosineSimilarity(c.Embedding.Span, questionEmbedding.AsSpan()) 
            })
            .OrderByDescending(x => x.Similarity)
            .Take(3)
            .ToList();

        var contextBuilder = new System.Text.StringBuilder();
        foreach (var c in topChunks)
        {
            contextBuilder.AppendLine(c.Chunk.Text);
            contextBuilder.AppendLine("---");
        }

        var systemPrompt = "Sen Fırat Üniversitesi Öğrenci Toplulukları yönetmeliği asistanısın. Aşağıdaki yönetmelik maddelerine (bağlama) göre sorulan soruyu yanıtla. Sadece sağlanan bağlamı kullan. Eğer cevap bağlamda yoksa 'Bununla ilgili yönetmelikte bir bilgi bulamadım' de. Yanıtını anlaşılır, Türkçe ve profesyonel bir dille ver.\n\nBağlam:\n" + contextBuilder.ToString();

        return await GenerateChatResponseAsync(systemPrompt, question, cancellationToken);
    }

    private async Task<string> GenerateChatResponseAsync(string systemPrompt, string userQuestion, CancellationToken cancellationToken)
    {
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";
        
        var requestBody = new
        {
            systemInstruction = new
            {
                parts = new[] { new { text = systemPrompt } }
            },
            contents = new[]
            {
                new 
                {
                    role = "user",
                    parts = new[] { new { text = userQuestion } }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(url, requestBody, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            return $"Gemini Chat Error: {error}";
        }

        using var jsonDoc = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken);
        var text = jsonDoc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        return text ?? "Cevap üretilemedi.";
    }
}
