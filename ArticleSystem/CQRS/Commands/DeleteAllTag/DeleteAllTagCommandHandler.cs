using ArticleSystem.Repository;
using MediatR;

namespace ArticleSystem.CQRS.Commands.DeleteAllTag
{
    public class DeleteAllTagCommandHandler : IRequestHandler<DeleteAllTagCommand>
    {
        private ITagRepository _tagRepository;

        public DeleteAllTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<Unit> Handle(DeleteAllTagCommand request, CancellationToken cancellationToken)
        {
            _tagRepository.DeleteAllTag(request._articleId);

            return Unit.Value;
        }
    }
}
