using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DTOs;
using ToDo.Services.ToDo;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public TodosController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }
        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks([FromQuery] FilterRequest filter)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault());
            return Ok(await _toDoService.GetTasks(userId, filter));
        }
        [HttpGet("tasks/{taskId}")]
        public async Task<IActionResult> GetTasksById(Guid taskId)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault());
            var item = await _toDoService.GetTaskById(taskId, userId);
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ToDoRequest toDo)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault());
            await _toDoService.CreateTask(userId, toDo);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ToDoRequest toDo, Guid taskId)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault());
            await _toDoService.UpdateTask(userId, toDo, taskId);
            return Ok(toDo);
        }
        [HttpPatch("tasks/complete")]
        public async Task<IActionResult> CompleteTask([FromBody] List<Guid> taskIDs)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault());
            await _toDoService.CompleteTask(userId, taskIDs);
            return Ok();
        }
    }
}
