using MediatR;

namespace ArticleSystem.CQRS.Commands.DeleteAllTag
{
    public class DeleteAllTagCommand : IRequest
    {
        public int _articleId { get; set; }
        public DeleteAllTagCommand(int articleId)
        {
            _articleId = articleId;
        }
    }
}
