using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.DTOs
{
    public class CommentAddDto
    {
        public int ArticleId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Content { get; set; } = default!;
    }
}
