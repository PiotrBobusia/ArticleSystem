using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.DTOs
{
    public class TagArtDto
    {
        [Required]
        public int ArticleId { get; set; } = default!;
    }
}
