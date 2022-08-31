using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ToDo.DTO;
using ToDo.Model;

namespace ToDo.Services.ToDo
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoDbContext _context;
        public ToDoService(ToDoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Model.ToDo>> GetTask(Guid userId, int status, int day)
        {
            if (status == 0 && day == 0)
            {
                return await _context.Tasks.ToListAsync();
            }

            if (status == 0 && day != 0)
            {
                return await _context.Tasks.Where(c => c.UserId == userId && c.Date.Day == day).ToListAsync();
            }

            if (status != 0 && day == 0)
            {
                return await _context.Tasks.Where(c => c.UserId == userId && c.Status == status).ToListAsync();

            }

            return await _context.Tasks.Where(c => c.UserId == userId && c.Date.Day == day && c.Status == status).ToListAsync();

        }

        public async Task<Model.ToDo> GetTaskById(Guid taskId)
        {
            return await _context.Tasks.FirstOrDefaultAsync(c => c.Id == taskId);
        }

        public async Task CreateTask(ToDoRequest toDoRequest)
        {
            Model.ToDo toDo = new Model.ToDo()
            {
                CategoryId = toDoRequest.CategoryId,
                UserId = toDoRequest.UserId,
                Title = toDoRequest.Title,
                Details = toDoRequest.Details,
                Date = DateTime.Now,
                Status = toDoRequest.Status
            };
            _context.Tasks.Add(toDo);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateTask(ToDoRequest toDo, Guid taskId)
        {
            var item = _context.Tasks.FirstOrDefault(c => c.Id == taskId);
            item.CategoryId = toDo.CategoryId;
            item.UserId = toDo.UserId;
            item.Details = toDo.Details;
            item.Title = toDo.Title;
            item.Date = DateTime.Now;
            item.Status = toDo.Status;
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task TaskDone(List<Guid> taskID)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    List<Model.ToDo> toDo = new List<Model.ToDo>();
                    foreach (var x in taskID)
                    {
                        var task = _context.Tasks.FirstOrDefault(c => c.Id == x);
                        task.Status = 2;
                        toDo.Add(task);
                    }
                    _context.UpdateRange(toDo);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }


        public async Task<List<Category>> GetCategory()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
