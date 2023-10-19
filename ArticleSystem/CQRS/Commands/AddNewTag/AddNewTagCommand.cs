using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Commands.AddNewTag
{
    public class AddNewTagCommand : TagAddDto, IRequest
    {
    }
}
