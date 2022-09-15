using ToDo.DTO;
using ToDo.DTOs;
using ToDo.Model;

namespace ToDo.Services.User
{
    public interface IUserService
    {
        List<UserResponse> GetUser();
        Task<UserResponse> GetByEmail(string email);
        Task<UserResponse> GetByUserName(string username);
        bool CheckLogin(LoginRequest loginRequest);
        Task SignUp(RegisterRequest registerRequest);
    }
}
