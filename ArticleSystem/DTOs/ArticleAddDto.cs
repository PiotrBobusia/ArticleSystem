using ArticleSystem.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.DTOs
{
    public class ArticleAddDto
    {
        [Required]
        [StringLength(60, MinimumLength = 15)]
        public string Title { get; set; } = default!;

        [Required]
        [MinLength(50)]
        public string Content { get; set; } = default!;

        [Required]
        public Category Category { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
