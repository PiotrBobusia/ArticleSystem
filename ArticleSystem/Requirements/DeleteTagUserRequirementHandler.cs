using ArticleSystem.DTOs;
using ArticleSystem.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ArticleSystem.Requirements
{
    public class DeleteTagUserRequirementHandler : AuthorizationHandler<DeleteTagUserRequirement, TagDeleteDto>
    {
        private IArticleRepository _articleRepository;

        public DeleteTagUserRequirementHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteTagUserRequirement requirement, TagDeleteDto tagDeleteDto)
        {
            var userRole = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = int.Parse(context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if(userRole == "Admin" || userRole == "Moderator")
            {
                context.Succeed(requirement);
            }

            var userArticles = _articleRepository.GetArticleByUserId(userId);

            if (userArticles.Any(x => x.Id == tagDeleteDto.ArticleId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
