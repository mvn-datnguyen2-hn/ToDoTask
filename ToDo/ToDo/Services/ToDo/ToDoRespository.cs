using Microsoft.EntityFrameworkCore;
using ToDo.Model;

namespace ToDo.Services.ToDo
{
    public class ToDoRespository : IToDoRespository
    {
        private readonly ToDoDbContext _context;
        public ToDoRespository(ToDoDbContext context)
        {
            _context = context;
        }
        public async Task<List<Model.ToDo>> GetTask()
        {
            return await _context.Tasks.ToListAsync();
        }
        public async Task<Model.ToDo> GetTaskById(string id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task CreateTask(Model.ToDo toDo)
        {
            _context.Tasks.Add(toDo);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateTask(Model.ToDo toDo)
        {
            _context.Update(toDo);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Model.ToDo>> GetTaskByStatus(int status)
        {
            return await _context.Tasks.Where(c => c.Status == status).ToListAsync();
        }
        public async Task<List<Model.ToDo>> GetTaskByDay(int day)
        {
            return await _context.Tasks.Where(c => c.Date.Day == day).ToListAsync();
        }
        public async Task<List<Category>> GetCategory()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
