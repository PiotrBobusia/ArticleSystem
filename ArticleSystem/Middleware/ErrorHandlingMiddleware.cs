using ArticleSystem.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace ArticleSystem.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (AccessDeniedException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (BadLoginException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NotExistException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (UserLoginDuplicateException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (UserEmailDuplicateException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NoArticleToModifyException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NoArticleToTaggedException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (TagAlreadyExistException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NoTagToRemoveException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NoCommentToRemoveException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
