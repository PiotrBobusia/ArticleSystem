using ArticleSystem.Entity;
using ArticleSystem.Exceptions;
using ArticleSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ArticleSystem.Requirements
{
    public class DeleteAllTagUserRequirementHandler : AuthorizationHandler<DeleteAllTagUserRequirement, int>
    {
        private IArticleRepository _articleRepository;

        public DeleteAllTagUserRequirementHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteAllTagUserRequirement requirement, int articleId)
        {
            var userRole = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = int.Parse(context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (userRole == "Admin" || userRole == "Moderator")
            {
                context.Succeed(requirement);
            }

            Article? potentialArticle =  _articleRepository.GetArticleById(articleId);

            if (potentialArticle is null) throw new NotExistException("Article not exist");

            if(potentialArticle.AuthorId == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
