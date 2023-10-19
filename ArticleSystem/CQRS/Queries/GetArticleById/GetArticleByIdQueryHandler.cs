using ArticleSystem.DTOs;
using ArticleSystem.Repository;
using ArticleSystem.Services;
using AutoMapper;
using MediatR;

namespace ArticleSystem.CQRS.Queries.GetArticleById
{
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleGetDto>
    {
        private IUserContextService _userContext;
        private IMapper _mapper;
        private IArticleRepository _articleRepository;

        public GetArticleByIdQueryHandler(IUserContextService userContext, IMapper mapper, IArticleRepository articleRepository)
        {
            _userContext = userContext;
            _mapper = mapper;
            _articleRepository = articleRepository;
        }
        public async Task<ArticleGetDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = _articleRepository.GetArticleById(request._articleId);

            var articleDto = _mapper.Map<ArticleGetWithCommentsDto>(article);

            return articleDto;
        }
    }
}
