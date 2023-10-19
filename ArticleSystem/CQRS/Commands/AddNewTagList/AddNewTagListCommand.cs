using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Commands.AddNewTagList
{
    public class AddNewTagListCommand : TagListAddDto, IRequest
    {
    }
}
