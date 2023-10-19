using System.ComponentModel.DataAnnotations;

namespace ArticleSystem.Entity
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;
    }
}
