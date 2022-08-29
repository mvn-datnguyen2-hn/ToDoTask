using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Services.ToDo;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TodoController : ControllerBase
    {
        private readonly IToDoRespository _respository;
    
        public TodoController(IToDoRespository respository)
        {
            _respository = respository;
            
        }
        [Authorize]
        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var item = await _respository.GetTask();
            return Ok(item);
        }
        [HttpGet("tasks/{id}")]
        public async Task<IActionResult> GetTasksById(string id)
        {
            var item = await _respository.GetTaskById(id);
            return Ok(item);
        }
        [HttpGet("tasks-status/{status}")]
        public async Task<IActionResult> GetTaskByStatus(int status)
        {
            var item = await _respository.GetTaskByStatus(status);
            return Ok(item);
        }
        [HttpGet("tasks-day/{day}")]
        public async Task<IActionResult> GetTaskByDay(int day)
        {
            var item = await _respository.GetTaskByDay(day);
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Model.ToDo toDo)
        {
            await _respository.CreateTask(toDo);
            return Ok(toDo);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Model.ToDo toDo, string id)
        {
            await _respository.UpdateTask(toDo);
            return Ok(toDo);
        }
        [HttpPut("tasks-done")]
        public async Task<IActionResult> CompleteTask(List<Model.ToDo> lstTodo)
        {
            foreach (var x in lstTodo)
            {
                x.Status = 1;
                await _respository.UpdateTask(x);
            }
            return Ok(lstTodo);
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var item = await _respository.GetCategory();
            return Ok(item);
        }

    }
}
