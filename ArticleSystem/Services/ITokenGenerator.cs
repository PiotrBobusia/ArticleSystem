using ArticleSystem.Entity;

namespace ArticleSystem.Services
{
    public interface ITokenGenerator
    {
        string generateToken(User user);
    }
}