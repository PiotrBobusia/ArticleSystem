using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using System.Security.Claims;

namespace ArticleSystem.Requirements
{
    public class AddNewArticleUserRequirementHandler : AuthorizationHandler<AddNewArticleUserRequirement, ArticleAddDto>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AddNewArticleUserRequirement requirement, ArticleAddDto article)
        {
            if (article.Category != Category.ForAdult && article.Category != Category.Premium)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userRole = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            if (article.Category == Category.Premium &&
                (userRole == "PremiumUser" ||
                 userRole == "Moderator" ||
                 userRole == "Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userBirthString = context.User.Claims.FirstOrDefault(x => x.Type == "Birth").Value;
            userBirthString = userBirthString?.Replace('.', '-');
            var userBirth = DateTime.ParseExact(userBirthString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var userAge = (int)((DateTime.Today - userBirth).TotalDays / 365.25);

            if (article.Category == Category.ForAdult && userAge >= 18)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
