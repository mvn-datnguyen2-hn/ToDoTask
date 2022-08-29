using Microsoft.EntityFrameworkCore;

namespace ToDo.Model
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
                    ID = "1",Name = "Work"
                },
                new Category
                {
                    ID = "2",
                    Name = "Family"
                },
                new Category
                {
                    ID = "3",
                    Name = "Birth"
                },
                new Category
                {
                    ID = "4",
                    Name = "Fun"
                },
                new Category
                {
                    ID = "5",
                    Name = "Sport"
                },
                new Category
                {
                    ID = "6",
                    Name = "Study"
                }
                );
            modelBuilder.Entity<ToDo>().HasData(
                new ToDo
                {
                    Id = "1",
                    Title = "Special",
                    Details = "Hello,I would like to take out something",
                    CategoryId = "1"
                },
                new ToDo
                {
                    Id = "2",
                    Title = "Go Home",
                    Details = "Qingming Festival holiday home",
                    CategoryId = "2"
                }, 
                new ToDo
                {
                    Id = "3",
                    Title = "Girlfriend's Birthday",
                    Details = "Remember to buy gift home",
                    CategoryId = "3"
                },
                new ToDo
                {
                    Id = "4",
                    Title = "Japan Tourism",
                    Details = "Remember to pack your luggage",
                    CategoryId = "2"
                }
            );
        }

        public DbSet<ToDo> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
