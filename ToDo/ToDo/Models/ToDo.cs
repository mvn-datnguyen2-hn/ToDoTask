using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models
{
    public class ToDo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        
    }
}
