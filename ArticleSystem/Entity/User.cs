using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticleSystem.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 5)]
        public string Login { get; set; } = default!;

        [Required]
        public string HashedPassword { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; } = default!;
        public Role Role { get; set; }

        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}