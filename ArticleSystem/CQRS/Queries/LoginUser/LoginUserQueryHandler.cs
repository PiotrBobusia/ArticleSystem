using ArticleSystem.Entity;
using ArticleSystem.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticleSystem.CQRS.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, User>
    {
        private IUserRepository _userRepository;

        public LoginUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            return _userRepository.LoginUser(request);
        }
    }
}
