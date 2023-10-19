using ArticleSystem.Exceptions;
using ArticleSystem.Repository;
using ArticleSystem.Services;
using MediatR;
using System.Globalization;
using System.Security.Claims;

namespace ArticleSystem.CQRS.Commands.AddNewArticle
{
    public class AddNewArticleCommandHandler : IRequestHandler<AddNewArticleCommand>
    {
        private IArticleRepository _articleRepository;
        private IUserContextService _userContext;

        public AddNewArticleCommandHandler(IArticleRepository articleRepository, IUserContextService userContext)
        {
            _articleRepository = articleRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(AddNewArticleCommand request, CancellationToken cancellationToken)
        {
            var userRole = _userContext.getClaims().FirstOrDefault(x => ClaimTypes.Role == x.Type).Value;

            if ((request.Category == Entity.Category.ForAdult && _userContext.isAdult())
                || (request.Category == Entity.Category.Premium && userRole != "PremiumUser")) throw new AccessDeniedException("Access denied");


            _articleRepository.AddNewArticle(request, int.Parse(_userContext.getUserId()));

            return Unit.Value;
        }
    }
}
