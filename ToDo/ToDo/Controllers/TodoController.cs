using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DTO;
using ToDo.Services.ToDo;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly IToDoService _toDoService;
    
        public TodoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }
        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks(Guid userId,[FromQuery] int status, [FromQuery] int day)
        {
            return Ok(await _toDoService.GetTask(userId, status, day));
        }
        [HttpGet("tasks/{taskId}")]
        public async Task<IActionResult> GetTasksById(Guid taskId)
        {
            var item = await _toDoService.GetTaskById(taskId);
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ToDoRequest toDo)
        {
            await _toDoService.CreateTask(toDo);
            return Ok(toDo);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ToDoRequest toDo, Guid taskId)
        {
            await _toDoService.UpdateTask(toDo,taskId);
            return Ok(toDo);
        }
        [HttpPut("tasks-done")]
        public async Task<IActionResult> CompleteTask([FromBody] List<Guid> taskID)
        {
            await _toDoService.TaskDone(taskID);
            return Ok();
        }


    }
}
