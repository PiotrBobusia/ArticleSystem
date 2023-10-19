using System.Globalization;
using System.Security.Claims;

namespace ArticleSystem.Services
{
    public class UserContextService : IUserContextService
    {
        private IHttpContextAccessor _contextAccessor;

        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? getUserId()
        {
            var userId = _contextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }

        public List<Claim>? getClaims()
        {
            var claimList = _contextAccessor.HttpContext?.User.Claims.ToList();
            return claimList;
        }

        public bool isAdult()
        {
            var userBirthString = getClaims()?.FirstOrDefault(x => x.Type == "Birth")?.Value;
            userBirthString = userBirthString?.Replace('.', '-');
            var userBirth = DateTime.ParseExact(userBirthString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var userAge = (int)((DateTime.Today - userBirth).TotalDays / 365.25);

            return userAge >= 18;
        }

    }
}
