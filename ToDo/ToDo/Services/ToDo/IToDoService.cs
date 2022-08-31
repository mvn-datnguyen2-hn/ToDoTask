using ToDo.DTO;
using ToDo.Model;

namespace ToDo.Services.ToDo
{
    public interface IToDoService
    {
        Task<List<Model.ToDo>> GetTask(Guid userId, int status, int day);
        Task<Model.ToDo> GetTaskById(Guid taskId);
        Task CreateTask(ToDoRequest toDo);
        Task UpdateTask(ToDoRequest toDo,Guid taskId);
        Task TaskDone(List<Guid> taskID);
        Task<List<Category>> GetCategory();
    }
}
