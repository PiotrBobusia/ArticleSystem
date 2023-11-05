using ArticleSystem.Database;
using ArticleSystem.Exceptions;
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
        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToDelete = _context.Comments.FirstOrDefault(x => x.Id == request._commentId);

            if (commentToDelete is null) throw new NoCommentToRemoveException("There's no Comment to delete");

            _context.Comments.Remove(commentToDelete);
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
