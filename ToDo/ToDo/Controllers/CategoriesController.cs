using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Services.ToDo;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public CategoriesController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var item = await _toDoService.GetCategory();
            return Ok(item);
        }
    }
}
