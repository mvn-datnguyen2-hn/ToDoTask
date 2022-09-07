namespace ToDo.Services.Category
{
    public interface ICategoryService
    {
        Task<List<Model.Category>> GetCategory();
    }
}
