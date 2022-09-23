namespace ToDo.Services.Category
{
    public interface ICategoryService
    {
        Task<List<Models.Category>> GetCategory();
    }
}
