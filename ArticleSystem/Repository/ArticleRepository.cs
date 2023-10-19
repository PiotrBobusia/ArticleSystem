using ArticleSystem.Database;
using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using ArticleSystem.Exceptions;
using ArticleSystem.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ArticleSystem.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private ArticleDbContext _context;
        private IMapper _mapper;

        public ArticleRepository(ArticleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddNewArticle(ArticleAddDto articleAddDto, int userId)
        {
            Article newArticle = _mapper.Map<Article>(articleAddDto);
            newArticle.AuthorId = userId;

            _context.Articles.Add(newArticle);
            _context.SaveChanges();
        }

        public List<Article> GetAllArticle()
        {
            var articleList = _context.Articles.Include(x => x.Author).Include(x => x.Tags).ToList();

            return articleList;
        }

        public Article GetArticleById(int articleId)
        {
            var result = _context.Articles.Include(x => x.Author).Include(x => x.Tags)
                .Include(x => x.Comments).ThenInclude(x => x.Author).FirstOrDefault(x => x.Id == articleId);

            if (result is null) throw new NotExistException("Article not exist");

            return result;
        }

        public Article GetArticleById(IUserContextService userContext, int articleId)
        {
            var result = _context.Articles.Include(x => x.Author).Include(x => x.Tags)
                .Include(x => x.Comments).ThenInclude(x => x.Author).FirstOrDefault(x => x.Id == articleId);

            if (result is null) throw new NotExistException("Article not exist");

            var userRole = "User";
            Boolean isAdult = false;

            if (userContext.getUserId() is not null)
            {
                userRole = userContext.getClaims().FirstOrDefault(x => ClaimTypes.Role == x.Type).Value;
                isAdult = userContext.isAdult();
            }

            if (result.Category == Category.ForAdult && !isAdult) throw new AccessDeniedException("Access Denied");
            if (result.Category == Category.Premium && userRole == "User") throw new AccessDeniedException("Access Denied");

            return result;
        }

        public List<Article> GetAllArticle(IUserContextService userContext)
        {
            var userRole = "User";

            if (userContext.getUserId() is not null)
            {
                userRole = userContext.getClaims().FirstOrDefault(x => ClaimTypes.Role == x.Type).Value;
            }

            var articleList = _context.Articles.Include(x => x.Author).Include(x => x.Tags)
                                    .Where(x => x.Category != Category.Premium || (userRole == "Premium" || userRole == "Admin" || userRole == "Moderator"));

            if (userContext.getUserId() is not null) articleList = articleList.Where(x => x.Category != Category.ForAdult || userContext.isAdult());

            var resultList = articleList.ToList();

            return resultList;
        }

        public List<Article> GetArticleByCat(IUserContextService userContext, string category)
        {
            var userRole = "User";

            if (userContext.getUserId() is not null)
            {
                userRole = userContext.getClaims().FirstOrDefault(x => ClaimTypes.Role == x.Type).Value;
            }

            if (category.ToLower() == Category.Premium.ToString().ToLower() && userRole == "User") throw new AccessDeniedException("Access denied");
            if (userContext is null || category.ToLower() == Category.ForAdult.ToString().ToLower() && !userContext.isAdult()) throw new AccessDeniedException("Access denied");

            Category enumCategory;

            try
            {
                enumCategory = Enum.Parse<Category>(category, ignoreCase: true);
            }
            catch (Exception ex)
            {
                enumCategory = Category.Other;
            }

            var articleList = _context.Articles.Include(x => x.Author)
                                    .Where(x => x.Category == enumCategory)
                                    .ToList();

            return articleList;
        }

        public List<Article> GetArticleByUserId(int userId)
        {
            var articleList = _context.Articles.Include(x => x.Author).Include(x => x.Tags).Where(x => x.AuthorId == userId).ToList();

            return articleList;
        }

        public List<Article> GetMyOwnArticles(IUserContextService userContext)
        {
            if (userContext.getUserId() is null) throw new Exception("Something goes wrong");

            var userId = int.Parse(userContext.getUserId());
            var articleList = _context.Articles.Include(x => x.Author).Include(x => x.Tags).Where(x => x.AuthorId == userId).ToList();

            return articleList;
        }

        public void ModifyArticle(ArticleModifyDto articleDto)
        {

            Article? potentialArticle = null;

            if ((articleDto.Id is not null && articleDto.Title is null && _context.Articles.Any(x => x.Id == articleDto.Id))
                || (articleDto.Title is not null && articleDto.Id is null && _context.Articles.Any(x => x.Title == articleDto.Title))
                || (articleDto.Title is not null && articleDto.Id is not null
                            && (potentialArticle = _context.Articles.FirstOrDefault(x => x.Title == articleDto.Title)) is not null
                            && potentialArticle.Id == articleDto.Id))
            {
                if (potentialArticle is null && articleDto.Id is not null) potentialArticle = _context.Articles.FirstOrDefault(x => x.Id == articleDto.Id);
                else if (potentialArticle is null && articleDto.Title is not null) potentialArticle = _context.Articles.FirstOrDefault(x => x.Title == articleDto.Title);
                _mapper.Map(articleDto, potentialArticle);

                _context.Articles.Update(potentialArticle);
                _context.SaveChanges();
            }
            else throw new NoArticleToModifyException("There's no article to modify");
        }

        public int GetAuthorId(int articleId)
        {
            var authorId = _context.Articles.FirstOrDefault(x => x.Id == articleId).AuthorId;

            return authorId;
        }

        public int GetAuthorId(string title)
        {
            var authorId = _context.Articles.FirstOrDefault(x => x.Title == title).AuthorId;

            return authorId;
        }

        public void DeleteArticleById(int articleId)
        {
            if (_context.Articles.Any(x => x.Id == articleId))
            {
                var articleToRemove = _context.Articles.Include(x => x.Tags).Include(x => x.Comments).FirstOrDefault(x => x.Id == articleId);
                _context.Tags.RemoveRange(articleToRemove.Tags);
                _context.Comments.RemoveRange(articleToRemove.Comments);
                _context.Articles.Remove(articleToRemove);
                _context.SaveChanges();
            }
            else throw new NoArticleToModifyException("There's no article with this Id");
        }

        public void DeleteArticleByTitle(string articleTitle)
        {
            if (_context.Articles.Any(x => x.Title == articleTitle))
            {
                var articleToRemove = _context.Articles.Include(x => x.Tags).Include(x => x.Comments).FirstOrDefault(x => x.Title == articleTitle);
                _context.Tags.RemoveRange(articleToRemove.Tags);
                _context.Comments.RemoveRange(articleToRemove.Comments);
                _context.Articles.Remove(articleToRemove);
                _context.SaveChanges();
            }
            else throw new NoArticleToModifyException("There's no article with this Title");
        }
    }
}
