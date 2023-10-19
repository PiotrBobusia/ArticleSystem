using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Commands.DeleteTag
{
    public class DeleteTagCommand : TagDeleteDto, IRequest
    {
    }
}
