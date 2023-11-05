using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using ArticleSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ArticleSystem.Requirements
{
    public class ModifyArticleUserRequirementHandler : AuthorizationHandler<ModifyArticleUserRequirement, ArticleModifyDto>
    {
        private IArticleRepository _articleRepository;

        public ModifyArticleUserRequirementHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ModifyArticleUserRequirement requirement, ArticleModifyDto article)
        {
            var userRole = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = int.Parse(context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if(userRole == "Admin" || userRole == "Moderator")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else if (article.Id is not null && _articleRepository.GetArticleByUserId(userId).Any(x => x.Id == article.Id))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else if (article.Title is not null && _articleRepository.GetArticleByUserId(userId).Any(x => x.Title == article.Title))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
