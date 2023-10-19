using ArticleSystem.DTOs;
using ArticleSystem.Entity;

namespace ArticleSystem.Repository
{
    public interface IUserRepository
    {
        void AddUser(UserRegisterDto userRegisterDto);
        User LoginUser(UserLoginDto userLoginDto);
    }
}