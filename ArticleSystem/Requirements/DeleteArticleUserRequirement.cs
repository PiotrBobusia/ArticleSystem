using Microsoft.AspNetCore.Authorization;

namespace ArticleSystem.Requirements
{
    public class DeleteArticleUserRequirement : IAuthorizationRequirement
    {
        public Boolean _byTitle = false;

        public DeleteArticleUserRequirement()
        {
            _byTitle = false;
        }

        public DeleteArticleUserRequirement(Boolean byTitle)
        {
            _byTitle = byTitle;
        }
    }
}
