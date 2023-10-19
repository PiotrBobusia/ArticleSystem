using ArticleSystem.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.DTOs
{
    public class UserRegisterDto
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Login { get; set; } = default!;

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = default!;
        public string? DateOfBirth { get; set; }
    }
}
