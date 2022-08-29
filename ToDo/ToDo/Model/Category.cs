using System.ComponentModel.DataAnnotations;

namespace ToDo.Model
{
    public class Category
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
