using ArticleSystem.DTOs;

namespace ArticleSystem.Repository
{
    public interface ITagRepository
    {
        void AddTag(TagAddDto tagDto);
        void AddTagRange(TagListAddDto tagsDto);
        void DeleteAllTag(int articleId);
        void DeleteTag(TagDeleteDto tagDto);
    }
}