using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ToDo.DTOs;
using ToDo.Models;

namespace ToDo.Services.ToDo
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoDbContext _context;
        private readonly IMapper _mapper;
        public ToDoService(ToDoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        enum State
        {
            NotComplete = 0,
            Done = 1,
        }
        public async Task<List<Models.ToDo>> GetTasks(Guid userId, FilterRequest filterRequest)
        {
            var querry = _context.Tasks.Where(c => c.UserId == userId);

            if (filterRequest.Status != null)
            {
                querry = querry.Where(c => c.Status == filterRequest.Status);
            }

            if (filterRequest.Day != null)
            {
                querry = querry.Where(c => c.Date.Date == filterRequest.Day);
            }

            return await querry.ToListAsync();
        }
        public async Task<Models.ToDo> GetTaskById(Guid taskId, Guid userId)
        {
            return await _context.Tasks.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == taskId);
        }
        public async Task CreateTask(Guid userId, ToDoRequest toDoRequest)
        {
            var item = _mapper.Map<ToDoRequest, Models.ToDo>(toDoRequest);
            item.UserId = userId;
            item.Date = DateTime.Now;
            item.Status = (int)State.NotComplete;
            _context.Tasks.Add(item);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTask(Guid userId, ToDoRequest toDo, Guid taskId)
        {
            var item = _context.Tasks.FirstOrDefault(c => c.UserId == userId && c.Id == taskId);
            if (item != null)
            {
                item.CategoryId = toDo.CategoryId;
                item.Details = toDo.Details;
                item.Title = toDo.Title;
                item.Date = DateTime.Now;
                _context.Update(item);
            }
            await _context.SaveChangesAsync();
        }
        public async Task CompleteTask(Guid userId, List<Guid> taskIDs)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                var task = await _context.Tasks.Where(c => taskIDs.Contains(c.Id) && c.UserId == userId).ToListAsync();
                foreach (var x in task)
                {
                    x.Status = (int)State.Done;
                }
                _context.UpdateRange(task);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
    }
}
