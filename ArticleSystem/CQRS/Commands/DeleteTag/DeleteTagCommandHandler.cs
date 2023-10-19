using ArticleSystem.Repository;
using MediatR;

namespace ArticleSystem.CQRS.Commands.DeleteTag
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private ITagRepository _tagRepository;

        public DeleteTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            _tagRepository.DeleteTag(request);

            return Unit.Value;
        }
    }
}
