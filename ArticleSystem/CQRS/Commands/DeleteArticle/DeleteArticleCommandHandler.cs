using ArticleSystem.Repository;
using MediatR;

namespace ArticleSystem.CQRS.Commands.DeleteArticle
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
    {
        private IArticleRepository _articleRepository;

        public DeleteArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            if(request._articleId is not null) _articleRepository.DeleteArticleById((int)request._articleId);
            else if (request._articleTitle is not null) _articleRepository.DeleteArticleByTitle((string)request._articleTitle);

            return Unit.Value;
        }
    }
}
