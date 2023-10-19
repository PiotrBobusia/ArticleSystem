using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Queries.GetAllArticles
{
    public class GetAllArticlesQuery : IRequest<IEnumerable<ArticleGetDto>>
    {
    }
}
