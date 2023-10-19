using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticleSystem.Entity
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public User? Author { get; set; } = default!;

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; } = default!;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Content { get; set; } = default!;

        public DateTime Date { get; set; }
        public DateTime? ModyficationDate { get; set; }
    }
}
