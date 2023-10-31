using MediatR;

namespace ArticleSystem.CQRS.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest
    {
        public int _commentId { get; set; }
        public DeleteCommentCommand(int commentId)
        {
            _commentId = commentId;
        }
    }
}
