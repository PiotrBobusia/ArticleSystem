using ArticleSystem.DTOs;

namespace ArticleSystem.Repository
{
    public interface ICommentRepository
    {
        void AddComment(CommentAddDto commentDto);
    }
}