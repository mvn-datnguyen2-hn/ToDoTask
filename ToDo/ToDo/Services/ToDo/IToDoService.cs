using ToDo.DTO;
using ToDo.DTOs;
using ToDo.Model;

namespace ToDo.Services.ToDo
{
    public interface IToDoService
    {
        Task<List<Model.ToDo>> GetTask(Guid userId, FilterRequest filterRequest);
        Task<Model.ToDo> GetTaskById(Guid taskId,Guid userId);
        Task CreateTask(Guid userId, ToDoRequest toDo);
        Task UpdateTask(Guid userId, ToDoRequest toDo);
        Task TaskDone(Guid userId, List<Guid> taskIDs);
        
    }
}
