using ArticleSystem.Database;
using ArticleSystem.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ArticleSystem.Requirements
{
    public class DeleteCommentUserRequirementHandler : AuthorizationHandler<DeleteCommentUserRequirement, int>
    {
        private ArticleDbContext _context;

        public DeleteCommentUserRequirementHandler(ArticleDbContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteCommentUserRequirement requirement, int commentId)
        {
            var userRole = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            var userId = int.Parse(context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if(userRole == "Admin" || userRole == "Moderator")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if(_context.Comments.Any(x => x.Id == commentId && x.AuthorId == userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
