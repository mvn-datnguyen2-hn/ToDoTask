using ToDo.Model;

namespace ToDo.Services.User
{
    public interface IUserRespository
    {
        Task<Model.User> GetByEmail(string email);
        Task<Model.User> GetByUserName(string username);
        Task<Model.User> CreateUser(Model.User user);
    }
}
