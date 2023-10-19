using ArticleSystem.Repository;
using MediatR;

namespace ArticleSystem.CQRS.Commands.AddNewTagList
{
    public class AddNewTagListCommandHandler : IRequestHandler<AddNewTagListCommand>
    {
        private ITagRepository _tagRepository;

        public AddNewTagListCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<Unit> Handle(AddNewTagListCommand request, CancellationToken cancellationToken)
        {
            _tagRepository.AddTagRange(request);
            return Unit.Value;
        }
    }
}
