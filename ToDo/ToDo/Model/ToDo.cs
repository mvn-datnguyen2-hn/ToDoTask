using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Model
{
    public class ToDo
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }

    }
}
