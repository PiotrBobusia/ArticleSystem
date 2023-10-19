using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using ArticleSystem.Services;

namespace ArticleSystem.Repository
{
    public interface IArticleRepository
    {
        void AddNewArticle(ArticleAddDto articleAddDto, int userId);
        void DeleteArticleById(int articleId);
        void DeleteArticleByTitle(string articleTitle);
        List<Article> GetAllArticle();
        List<Article> GetAllArticle(IUserContextService userContext);
        List<Article> GetArticleByCat(IUserContextService userContext, string category);
        Article GetArticleById(int articleId);
        Article GetArticleById(IUserContextService userContext, int articleId);
        List<Article> GetArticleByUserId(int userId);
        int GetAuthorId(int articleId);
        int GetAuthorId(string title);
        List<Article> GetMyOwnArticles(IUserContextService userContext);
        void ModifyArticle(ArticleModifyDto articleDto);
    }
}