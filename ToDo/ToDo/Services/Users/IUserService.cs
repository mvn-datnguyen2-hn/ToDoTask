using ToDo.DTO;
using ToDo.Model;

namespace ToDo.Services.User
{
    public interface IUserService
    {
        List<Model.User> GetUser();
        Task<Model.User> GetByEmail(string email);
        Task<Model.User> GetByUserName(string username);
        Task SignUp(RegisterRequest registerRequest);
    }
}
