using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.DTOs
{
    public class TagListAddDto : TagArtDto
    {
        [Required]
        public List<string> Value { get; set; } = new List<string>();
    }
}
