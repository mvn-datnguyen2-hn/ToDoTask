using ToDo.DTOs;
using ToDo.Models;

namespace ToDo.Services.Users
{
    public interface IUserService
    { 
        Task<UserResponse> GetByEmail(string email);
        Task<UserResponse> GetByUserName(string username);
        bool CheckLogin(LoginRequest loginRequest);
        Task SignUp(RegisterRequest registerRequest);
    }
}
