using ArticleSystem.CQRS.Commands.AddNewUser;
using ArticleSystem.CQRS.Queries.LoginUser;
using ArticleSystem.DTOs;
using ArticleSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArticleSystem.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : Controller
    {
        private IMediator _mediator;
        private ITokenGenerator _tokenGeneretor;

        public AccountController(IMediator mediator, ITokenGenerator tokenGeneretor)
        {
            _mediator = mediator;
            _tokenGeneretor = tokenGeneretor;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]AddNewUserCommand userRegisterDto)
        {
            await _mediator.Send(userRegisterDto);

            return Ok();
        }

        [HttpGet("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserQuery userLoginDto)
        {
            var user = await _mediator.Send(userLoginDto);

            var token = _tokenGeneretor.generateToken(user);

            return Ok($"Zostałeś poprawnie zalogowany\nOto twój token:\n{token}");
        }
    }
}
