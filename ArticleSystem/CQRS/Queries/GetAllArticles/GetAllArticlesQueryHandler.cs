using ArticleSystem.DTOs;
using ArticleSystem.Repository;
using ArticleSystem.Services;
using AutoMapper;
using MediatR;

namespace ArticleSystem.CQRS.Queries.GetAllArticles
{
    public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, IEnumerable<ArticleGetDto>>
    {
        private IArticleRepository _articleRepository;
        private IMapper _mapper;
        private IUserContextService _userContext;

        public GetAllArticlesQueryHandler(IArticleRepository articleRepository, IMapper mapper, IUserContextService userContext)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<IEnumerable<ArticleGetDto>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
        {

            var articleList = _articleRepository.GetAllArticle(_userContext);
            
            var articleDtoList = _mapper.Map<IEnumerable<ArticleGetDto>>(articleList);

            return articleDtoList;
        }
    }
}
