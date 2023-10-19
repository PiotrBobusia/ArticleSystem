using ArticleSystem.Entity;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ArticleSystem.DTOs
{
    public class ArticleGetDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public string Content { get; set; } = default!;

        public DateTime Date { get; set; }

        public string AuthorLogin { get; set; }

        [Required]
        public Category Category { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
