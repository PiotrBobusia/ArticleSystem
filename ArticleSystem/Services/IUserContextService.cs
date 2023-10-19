using System.Security.Claims;

namespace ArticleSystem.Services
{
    public interface IUserContextService
    {
        List<Claim>? getClaims();
        string? getUserId();
        bool isAdult();
    }
}