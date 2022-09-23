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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    ID = 1,
                    Name = "Work"
                },
                new Category
                {
                    ID = 2,
                    Name = "Family"
                },
                new Category
                {
                    ID = 3,
                    Name = "Birth"
                },
                new Category
                {
                    ID = 4,
                    Name = "Fun"
                },
                new Category
                {
                    ID = 5,
                    Name = "Sport"
                },
                new Category
                {
                    ID = 6,
                    Name = "Study"
                }
            );
        }


        public DbSet<ToDo> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
