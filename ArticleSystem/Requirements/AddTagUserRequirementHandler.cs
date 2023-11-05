using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using ArticleSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ArticleSystem.Requirements
{
    public class AddTagUserRequirementHandler : AuthorizationHandler<AddTagUserRequirement, TagArtDto>
    {
        private IArticleRepository _articleRepository;

        public AddTagUserRequirementHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AddTagUserRequirement requirement, TagArtDto tagDto)
        {
            var userRole = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = int.Parse(context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (userRole == "Admin" || userRole == "Moderator")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var article = _articleRepository.GetArticleById(tagDto.ArticleId);
            if(article.AuthorId == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
