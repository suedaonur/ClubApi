using Application.Features.Students.Commands.CreateStudent;
using Application.Features.Students.Commands.DeleteStudent;
using Application.Features.Students.Commands.LoginStudent;
using Application.Features.Students.Commands.RegisterStudent;
using Application.Features.Students.Commands.UpdateStudent;
using Application.Features.Students.Queries.GetAllStudents;
using Application.Features.Students.Queries.GetByIdStudent;
using ClubUI.Application.Features.Students.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetByIdStudentQuery { Id = id });
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { Id = id });
            return Ok(result);
        }
        [HttpPost("verify-obs")]
        public async Task<IActionResult> VerifyObs([FromBody] VerifyObsCommand command)
        {
            
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok(new
                {
                    success = true,
                    message = "OBS Doğrulaması Başarılı! Sisteme giriş yapabilirsiniz."
                });
            }

            
            return NotFound(new
            {
                success = false,
                message = "Öğrenci numarası OBS sisteminde bulunamadı!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterStudentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(new { message = "Kayıt başarıyla tamamlandı! Giriş yapabilirsiniz." });
            }
            return BadRequest(new { message = "Kayıt başarısız. Öğrenci numarası bulunamadı veya zaten kayıtlı." });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginStudentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result); 
            }

            return Unauthorized(new { message = "Öğrenci numarası veya şifre hatalı!" });
        }

    }
}
