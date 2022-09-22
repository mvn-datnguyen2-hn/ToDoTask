using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Models
{
    public class ToDoDbContext:Microsoft.EntityFrameworkCore.DbContext
    {
        public ToDoDbContext()
        {

        }
        public ToDoDbContext(DbContextOptions<ToDoDbContext> sqlDbContextOptions) : base(sqlDbContextOptions)
        {

        }
        public DbSet<ToDo> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
