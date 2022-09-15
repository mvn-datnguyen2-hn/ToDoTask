using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Services.Category
{
    public class CategoryService:ICategoryService
    {
        private readonly ToDoDbContext _context;
        public CategoryService(ToDoDbContext context)
        {
            _context = context;
        }
        public async Task<List<Models.Category>> GetCategory()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
