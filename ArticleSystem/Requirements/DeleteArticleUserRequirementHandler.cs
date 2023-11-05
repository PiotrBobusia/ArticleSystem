using ArticleSystem.Entity;
using ArticleSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ArticleSystem.Requirements
{
    public class DeleteArticleUserRequirementHandler : AuthorizationHandler<DeleteArticleUserRequirement, string>
    {
        private IArticleRepository _articleRepository;

        public DeleteArticleUserRequirementHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteArticleUserRequirement requirement, string articleIdTitle)
        {
            var userRole = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = int.Parse(context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (userRole == "Admin" || userRole == "Moderator")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else if (!requirement._byTitle && _articleRepository.GetArticleByUserId(userId).Any(x => x.Id == int.Parse(articleIdTitle)))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else if (requirement._byTitle && _articleRepository.GetArticleByUserId(userId).Any(x => x.Title.ToLower().Equals(articleIdTitle.ToLower())))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
