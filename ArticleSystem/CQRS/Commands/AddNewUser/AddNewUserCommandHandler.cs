using ArticleSystem.Repository;
using MediatR;

namespace ArticleSystem.CQRS.Commands.AddNewUser
{
    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand>
    {
        private IUserRepository _userRepository;

        public AddNewUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            _userRepository.AddUser(request);

            return Unit.Value;
        }
    }
}
