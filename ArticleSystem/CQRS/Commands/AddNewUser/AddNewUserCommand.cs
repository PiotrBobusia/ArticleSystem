using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticleSystem.CQRS.Commands.AddNewUser
{
    public class AddNewUserCommand : UserRegisterDto, IRequest
    {

    }
}
