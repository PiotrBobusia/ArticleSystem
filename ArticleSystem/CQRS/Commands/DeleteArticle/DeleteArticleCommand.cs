using MediatR;

namespace ArticleSystem.CQRS.Commands.DeleteArticle
{
    public class DeleteArticleCommand : IRequest
    {
        public int? _articleId = null; 
        public string? _articleTitle = null;

        public DeleteArticleCommand(int articleId)
        {
            _articleId = articleId;
        }

        public DeleteArticleCommand(string articleTitle)
        {
            _articleTitle = articleTitle;
        }
    }
}
