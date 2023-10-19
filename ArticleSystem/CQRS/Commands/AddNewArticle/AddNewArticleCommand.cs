using ArticleSystem.DTOs;
using MediatR;

namespace ArticleSystem.CQRS.Commands.AddNewArticle
{
    public class AddNewArticleCommand : ArticleAddDto, IRequest
    {
    }
}
