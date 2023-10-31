using ArticleSystem.Database;
using MediatR;

namespace ArticleSystem.CQRS.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private ArticleDbContext _context;

        public DeleteCommentCommandHandler(ArticleDbContext context)
        {
            _context = context;
        }
        public Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
