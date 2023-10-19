using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Queries.GetArticlesByCat
{
    public class GetArticlesByCatQuery : IRequest<IEnumerable<ArticleGetDto>>
    {
        public string Category { get; set; }
        public GetArticlesByCatQuery(string category)
        {
            Category = category;
        }
    }
}
