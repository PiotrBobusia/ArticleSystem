using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticleSystem.Entity
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; } = default!;

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; } = default!;
    }
}
