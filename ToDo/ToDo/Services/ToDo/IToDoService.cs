using ToDo.DTOs;
using ToDo.Models;

namespace ToDo.Services.ToDo
{
    public interface IToDoService
    {
        Task<List<Models.ToDo>> GetTasks(Guid userId, FilterRequest filterRequest);
        Task<Models.ToDo> GetTaskById(Guid taskId,Guid userId);
        Task CreateTask(Guid userId, ToDoRequest toDo);
        Task UpdateTask(Guid userId, ToDoRequest toDo, Guid taskId);
        Task CompleteTask(Guid userId, List<Guid> taskIDs);
    }
}
