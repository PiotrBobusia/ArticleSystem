

namespace ArticleSystem.DTOs
{
    public class ArticleCommentDto
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorLogin { get; set; }
        public string Content { get; set; } = default!;
        public DateTime Date { get; set; }
        public DateTime? ModyficationDate { get; set; }
    }
}
