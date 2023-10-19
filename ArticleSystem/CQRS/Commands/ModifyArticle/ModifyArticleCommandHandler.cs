using ArticleSystem.Repository;
using MediatR;

namespace ArticleSystem.CQRS.Commands.ModifyArticle
{
    public class ModifyArticleCommandHandler : IRequestHandler<ModifyArticleCommand>
    {
        private IArticleRepository _articleRepository;

        public ModifyArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<Unit> Handle(ModifyArticleCommand request, CancellationToken cancellationToken)
        {
            _articleRepository.ModifyArticle(request);

            return Unit.Value;
        }
    }
}
