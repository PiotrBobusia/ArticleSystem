using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ArticleSystem.DTOs
{
    public class UserLoginDto
    {
        [Required]
        public string Login { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
