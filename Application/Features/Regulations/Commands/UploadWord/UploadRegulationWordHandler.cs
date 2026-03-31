using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using DocumentFormat.OpenXml.Packaging;
using System.Text;
//ai la yaptm.
namespace Application.Features.Regulations.Commands.UploadWord;

public class UploadRegulationWordHandler : IRequestHandler<UploadRegulationWordCommand, int>
{
    private readonly IGenericRepository<Regulation> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadRegulationWordHandler(IGenericRepository<Regulation> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UploadRegulationWordCommand request, CancellationToken cancellationToken)
    {
        StringBuilder sb = new StringBuilder();

        // 1. Word dosyasını bellek üzerinden açıyoruz
        using (var stream = request.File.OpenReadStream())
        using (var wordDoc = WordprocessingDocument.Open(stream, false))
        {
            var body = wordDoc.MainDocumentPart.Document.Body;
            // Tüm paragrafları bulup metinlerini birleştiriyoruz
            foreach (var paragraph in body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
            {
                sb.AppendLine(paragraph.InnerText);
            }
        }

        // 2. Elde ettiğimiz metni daha önce kurduğumuz Regulation entity'sine veriyoruz
        var regulation = new Regulation
        {
            Title = request.Title,
            Category = request.Category,
            TextContent = sb.ToString() // Artık koca dosya tek bir string oldu!
        };

        await _repository.AddAsync(regulation);
        await _unitOfWork.SaveChangesAsync();

        return regulation.Id;
    }
}
