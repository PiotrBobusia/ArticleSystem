using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Commands.ModifyArticle
{
    public class ModifyArticleCommand : ArticleModifyDto, IRequest
    {
    }
}
