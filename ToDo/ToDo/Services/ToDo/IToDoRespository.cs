using ToDo.Model;

namespace ToDo.Services.ToDo
{
    public interface IToDoRespository
    {
        Task<List<Model.ToDo>> GetTask();
        Task<Model.ToDo> GetTaskById(string id);
        Task CreateTask(Model.ToDo toDo);
        Task UpdateTask(Model.ToDo toDo);
        Task<List<Model.ToDo>> GetTaskByStatus(int status);
        Task<List<Model.ToDo>> GetTaskByDay(int day);
        Task<List<Category>> GetCategory();
    }
}
