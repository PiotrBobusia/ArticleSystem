using ArticleSystem.DTOs;
using ArticleSystem.Repository;
using ArticleSystem.Services;
using AutoMapper;
using MediatR;

namespace ArticleSystem.CQRS.Queries.GetArticlesByCat
{
    public class GetArticlesByCatQueryHandler : IRequestHandler<GetArticlesByCatQuery, IEnumerable<ArticleGetDto>>
    {
        private IArticleRepository _articleRepository;
        private IUserContextService _userContext;
        private IMapper _mapper;

        public GetArticlesByCatQueryHandler(IArticleRepository articleRepository, IMapper mapper, IUserContextService userContext)
        {
            _articleRepository = articleRepository;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ArticleGetDto>> Handle(GetArticlesByCatQuery request, CancellationToken cancellationToken)
        {
            var articleList = _articleRepository.GetArticleByCat(_userContext, request.Category);

            var articleDtoList = _mapper.Map<IEnumerable<ArticleGetDto>>(articleList);

            return articleDtoList;
        }
    }
}
