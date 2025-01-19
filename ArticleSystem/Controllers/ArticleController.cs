using ArticleSystem.CQRS.Commands.AddComment;
using ArticleSystem.CQRS.Commands.AddNewArticle;
using ArticleSystem.CQRS.Commands.AddNewTag;
using ArticleSystem.CQRS.Commands.AddNewTagList;
using ArticleSystem.CQRS.Commands.DeleteAllTag;
using ArticleSystem.CQRS.Commands.DeleteArticle;
using ArticleSystem.CQRS.Commands.DeleteComment;
using ArticleSystem.CQRS.Commands.DeleteTag;
using ArticleSystem.CQRS.Commands.ModifyArticle;
using ArticleSystem.CQRS.Queries.GetAllArticles;
using ArticleSystem.CQRS.Queries.GetArticleById;
using ArticleSystem.CQRS.Queries.GetArticlesByCat;
using ArticleSystem.CQRS.Queries.GetArticlesByUser;
using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using ArticleSystem.Exceptions;
using ArticleSystem.Repository;
using ArticleSystem.Requirements;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleSystem.Controllers
{
    [ApiController]
    [Route("article")]
    public class ArticleController : Controller
    {
        private IMediator _mediator;
        private IAuthorizationService _authorizationService;

        public ArticleController(IMediator mediator, IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult> AddArticle([FromBody]AddNewArticleCommand articleAddDto)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(HttpContext.User, articleAddDto , new AddNewArticleUserRequirement()).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new AccessDeniedException("Access Denied");
            }
            await _mediator.Send(articleAddDto);
            return Ok("The article has been successfully added");
        }

        [HttpGet("get-{id}")]
        public async Task<ActionResult> GetArticleFromId([FromRoute]int id)
        {
            var articleList = await _mediator.Send(new GetArticleByIdQuery(id));
            
            return Ok(articleList);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAllArticles()
        {
            var articleList = await _mediator.Send(new GetAllArticlesQuery());

            return Ok(articleList);
        }

        [HttpGet("get-cat-{category}")]
        public async Task<ActionResult> GetArticlesByCategory([FromRoute]string category)
        {
            var articleList = await _mediator.Send(new GetArticlesByCatQuery(category));

            return Ok(articleList);
        }

        [Authorize]
        [HttpGet("get-my")]
        public async Task<ActionResult> GetOwnArticles()
        {
            var articleList = await _mediator.Send(new GetArticlesByUserQuery());

            return Ok(articleList);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet("userid-{id}")]
        public async Task<ActionResult> GetUserArticles([FromRoute]int id)
        {
            var articleList = await _mediator.Send(new GetArticlesByUserQuery(id));

            return Ok(articleList);
        }

        [Authorize]
        [HttpPatch("modify")]
        public async Task<ActionResult> ModifyArticle([FromBody]ModifyArticleCommand articleModifyDto)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(HttpContext.User, articleModifyDto, new ModifyArticleUserRequirement()).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new AccessDeniedException("Access Denied");
            }

            await _mediator.Send(articleModifyDto);

            return Ok("The article has been successfully modified");
        }


        [Authorize]
        [HttpDelete("delete-{articleId}")]
        public async Task<ActionResult> DeleteArticleById([FromRoute]string articleId)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(HttpContext.User, articleId, new DeleteArticleUserRequirement()).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new AccessDeniedException("Access Denied");
            }

            await _mediator.Send(new DeleteArticleCommand(articleId));

            return Ok("The article has been successfully deleted");
        }

        [Authorize]
        [HttpDelete("delete-by-title")]
        public async Task<ActionResult> DeleteArticleByTitle([FromBody]string articleTitle)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(HttpContext.User, articleTitle, new DeleteArticleUserRequirement(true)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new AccessDeniedException("Access Denied");
            }

            await _mediator.Send(new DeleteArticleCommand(articleTitle));

            return Ok("The article has been successfully deleted");
        }

        [Authorize]
        [HttpPost("tags/add")]
        public async Task<ActionResult> AddTag([FromBody]AddNewTagCommand tagAddDto)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(HttpContext.User, tagAddDto, new AddTagUserRequirement());
            if (!authorizationResult.Succeeded)
            {
                throw new AccessDeniedException("Access Denied");
            }

            await _mediator.Send(tagAddDto);

            return Ok("The tag has been successfully added");
        }


        [Authorize]
        [HttpPost("tags/add-list")]
        public async Task<ActionResult> AddTagList([FromBody]AddNewTagListCommand tagAddDto)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(HttpContext.User, tagAddDto, new AddTagUserRequirement()).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new AccessDeniedException("Access Denied");
            }

            await _mediator.Send(tagAddDto);

            return Ok("The tags has been successfully added");
        }

        [Authorize]
        [HttpDelete("tags/delete")]
        public async Task<ActionResult> DeleteTag([FromBody]DeleteTagCommand tagDeleteDto)
        {

            var authorizationResult = _authorizationService.AuthorizeAsync(HttpContext.User, tagDeleteDto, new DeleteTagUserRequirement()).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new AccessDeniedException("Access Denied");
            }

            await _mediator.Send(tagDeleteDto);

            return Ok("The tag has been successfully deleted");
        }

        [Authorize]
        [HttpDelete("tags/delete-all-{articleId}")]
        public async Task<ActionResult> DeleteAllTag([FromRoute] int articleId)
        {
                var authorizationResult = await _authorizationService.AuthorizeAsync(HttpContext.User, articleId, new DeleteAllTagUserRequirement());
                if (!authorizationResult.Succeeded)
                {
                    throw new AccessDeniedException("Access Denied");
                }

                await _mediator.Send(new DeleteAllTagCommand(articleId));

                return Ok("The tag has been successfully deleted");
        }

        [Authorize]
        [HttpPost("comment/add")]
        public async Task<ActionResult> AddComment([FromBody]AddCommentCommand commentAddDto)
        {
            await _mediator.Send(commentAddDto);

            return Ok("The comment has ben successfully added");
        }

        [Authorize]
        [HttpDelete("comment/delete-{commentId}")]
        public async Task<ActionResult> DeleteComment([FromQuery]int commentId)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(HttpContext.User, commentId, new DeleteCommentUserRequirement());
            if (!authorizationResult.Succeeded)
            {
                throw new AccessDeniedException("Access Denied");
            }

            await _mediator.Send(new DeleteCommentCommand(commentId));

            return Ok("The comment has ben successfully deleted");
        }

    }
}



/*
Dodawanie artukułu (zalogowani, do odpowiednich kategorii muszą mieć dostęp) [X]
Modyfikacja artykułu przez id (twórca, moderator, admin) [X]
Modyfikacja artykułu przez tytuł (twórca, moderator, admin) [X]
Pobieranie artykułów z wybranej kategorii (Kategorie po wieku/Kategorie premium) (nie zalogowani też mogą wyświetlać ogólnodostepne artykuły) [X]
Pobieranie swoich artykułów [X]
Pobieranie treści artukułu po Id [X]
Usuwanie artykułu (twórca, moderator, admin) [X]

Tag Add [x]
TagRange Add [x]

Tag Del [x]
AllTag Del [x]

Dodawanie komantarzy (zalogowani, jeśli mają kryteria wiekowe itp)
Usuwanie komentarzy (twórca artukułu, twórca komentarza, moderator, admin)
Edycja komentarza (twórca komentarza)

User nie może duplikować Loginu/Emaila [X]
*/