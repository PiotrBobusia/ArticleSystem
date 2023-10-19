using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Queries.GetArticlesByUser
{
    public class GetArticlesByUserQuery : IRequest<IEnumerable<ArticleGetDto>>
    {
        public int? _userId { get; set; }
        public GetArticlesByUserQuery()
        {
            _userId = null;
        }

        public GetArticlesByUserQuery(int? userId)
        {
            _userId = userId;
        }
    }
}
