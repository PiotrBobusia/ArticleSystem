using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.DTOs
{
    public class TagAddDto : TagArtDto
    {

        [Required]
        public string Value { get; set; } = default!;
    }
}
