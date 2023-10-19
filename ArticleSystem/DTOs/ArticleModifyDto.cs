using ArticleSystem.Entity;
using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.DTOs
{
    public class ArticleModifyDto
    {
        public int? Id { get; set; }

        [StringLength(60, MinimumLength = 15)]
        public string? Title { get; set; }

        [MinLength(50)]
        public string? Content { get; set; } = default!;

        public Category? Category { get; set; }

        public List<Tag>? Tags { get; set; }
    }
}
