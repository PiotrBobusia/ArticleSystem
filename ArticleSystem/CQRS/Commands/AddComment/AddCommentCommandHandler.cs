using ArticleSystem.Repository;
using MediatR;

namespace ArticleSystem.CQRS.Commands.AddComment
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand>
    {
        private ICommentRepository _commentRepository;

        public AddCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            _commentRepository.AddComment(request);

            return Unit.Value;
        }
    }
}
