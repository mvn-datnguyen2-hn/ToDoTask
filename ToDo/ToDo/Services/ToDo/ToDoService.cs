using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ToDo.DTO;
using ToDo.DTOs;
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

        public async Task<List<Model.ToDo>> GetTask(Guid userId, FilterRequest filterRequest)
        {
            if (filterRequest.Status == null && filterRequest.Day == null)
            {
                return await _context.Tasks.Where(c => c.UserId == userId).ToListAsync();
            }

            if (filterRequest.Status == null && filterRequest.Day != null)
            {
                return await _context.Tasks.Where(c => c.UserId == userId && c.Date.Date == filterRequest.Day).ToListAsync();
            }

            if (filterRequest.Status != null && filterRequest.Day == null)
            {
                return await _context.Tasks.Where(c => c.UserId == userId && c.Status == filterRequest.Status).ToListAsync();
            }

            return await _context.Tasks.Where(c => c.UserId == userId && c.Date == filterRequest.Day && c.Status == filterRequest.Status).ToListAsync();

        }
        public async Task<Model.ToDo> GetTaskById(Guid taskId, Guid userId)
        {
            return await _context.Tasks.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == taskId);
        }
        public async Task CreateTask(Guid userId, ToDoRequest toDoRequest)
        {
            Model.ToDo toDo = new Model.ToDo()
            {
                Id = toDoRequest.TaskId,
                CategoryId = toDoRequest.CategoryId,
                UserId = userId,
                Title = toDoRequest.Title,
                Details = toDoRequest.Details,
                Date = DateTime.Now,
                Status = toDoRequest.Status
            };
            _context.Tasks.Add(toDo);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateTask(Guid userId, ToDoRequest toDo)
        {
            var item = _context.Tasks.FirstOrDefault(c => c.Id == toDo.TaskId);
            item.CategoryId = toDo.CategoryId;
            item.UserId = userId;
            item.Details = toDo.Details;
            item.Title = toDo.Title;
            item.Date = DateTime.Now;
            item.Status = toDo.Status;
            _context.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task TaskDone(Guid userId, List<Guid> taskIDs)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                var task = await _context.Tasks.Where(c => taskIDs.Contains(c.Id) && c.UserId == userId).ToListAsync();
                foreach (var x in task)
                {
                    x.Status = 2;
                }
                _context.UpdateRange(task);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }


    }
}
