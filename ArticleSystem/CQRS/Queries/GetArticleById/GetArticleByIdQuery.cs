using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Queries.GetArticleById
{
    public class GetArticleByIdQuery : IRequest<ArticleGetDto>
    {
        public int _articleId;

        public GetArticleByIdQuery(int id)
        {
            _articleId = id;
        }
    }
}
