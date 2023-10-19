using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Commands.AddComment
{
    public class AddCommentCommand : CommentAddDto, IRequest
    {
    }
}
