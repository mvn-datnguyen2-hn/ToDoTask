using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        public ICollection<ToDo> Tasks { get; set; }
    }
}
