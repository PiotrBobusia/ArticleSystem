using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using MediatR;

namespace ArticleSystem.CQRS.Queries.LoginUser
{
    public class LoginUserQuery : UserLoginDto, IRequest<User>
    {

    }
}
