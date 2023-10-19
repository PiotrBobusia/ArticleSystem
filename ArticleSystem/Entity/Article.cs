using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ArticleSystem.Entity
{

    public enum Category
    {
        IT, Technologies, Health, Travel, Finance, ForKids, ForAdult, Premium, Other
    }
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 15)]
        public string Title { get; set; } = default!;

        [Required]
        [MinLength(50)]
        public string Content { get; set; } = default!;

        public DateTime Date { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public User Author { get; set; } = default!;

        [Required]
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
