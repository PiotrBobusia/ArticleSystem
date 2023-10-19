using ArticleSystem.Repository;
using MediatR;

namespace ArticleSystem.CQRS.Commands.AddNewTag
{
    public class AddNewTagCommandHandler : IRequestHandler<AddNewTagCommand>
    {
        private ITagRepository _tagRepository;

        public AddNewTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<Unit> Handle(AddNewTagCommand request, CancellationToken cancellationToken)
        {
            _tagRepository.AddTag(request);

            return Unit.Value;
        }
    }
}
