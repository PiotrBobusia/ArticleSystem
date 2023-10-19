
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ArticleSystem.DTOs
{
    public class ArticleGetWithCommentsDto : ArticleGetDto
    {
        [JsonPropertyOrder(1)]
        public List<ArticleCommentDto> Comments { get; set; } = new List<ArticleCommentDto>();
    }
}
