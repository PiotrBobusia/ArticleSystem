using ArticleSystem.Database;
using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using ArticleSystem.Services;
using AutoMapper;

namespace ArticleSystem.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private ArticleDbContext _context;
        private IUserContextService _userContext;
        private IMapper _mapper;

        public CommentRepository(ArticleDbContext context, IUserContextService userContext, IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
        }

        public void AddComment(CommentAddDto commentDto)
        {
            Comment newComment = _mapper.Map<Comment>(commentDto);

            newComment.AuthorId = int.Parse(_userContext.getUserId());

            _context.Comments.Add(newComment);
            _context.SaveChanges();
        }
    }
}
