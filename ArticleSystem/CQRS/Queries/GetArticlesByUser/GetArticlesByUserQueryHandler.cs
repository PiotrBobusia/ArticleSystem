using ArticleSystem.DTOs;
using ArticleSystem.Repository;
using ArticleSystem.Services;
using AutoMapper;
using MediatR;

namespace ArticleSystem.CQRS.Queries.GetArticlesByUser
{
    public class GetArticlesByUserQueryHandler : IRequestHandler<GetArticlesByUserQuery, IEnumerable<ArticleGetDto>>
    {
        private IUserContextService _userContext;
        private IArticleRepository _articleRepository;
        private IMapper _mapper;

        public GetArticlesByUserQueryHandler(IUserContextService userContext, IMapper mapper, IArticleRepository articleRepository)
        {
            _userContext = userContext;
            _articleRepository = articleRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ArticleGetDto>> Handle(GetArticlesByUserQuery request, CancellationToken cancellationToken)
        {
            if(request._userId is not null)
            {
                var articleList = _articleRepository.GetArticleByUserId(request._userId.GetValueOrDefault());
                var articleDtoList = _mapper.Map<IEnumerable<ArticleGetDto>>(articleList);

                return articleDtoList;
            }
            else
            {
                var articleList = _articleRepository.GetArticleByUserId(int.Parse(_userContext.getUserId()));
                var articleDtoList = _mapper.Map<IEnumerable<ArticleGetDto>>(articleList);

                return articleDtoList;
            }
        }
    }
}
