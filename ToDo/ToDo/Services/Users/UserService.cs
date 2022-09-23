
using AutoMapper;
using ToDo.DTOs;
using ToDo.Models;
using ToDo.Services.PasswordHash;

namespace ToDo.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ToDoDbContext _context;
        private readonly BcryptPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public UserService(ToDoDbContext context, BcryptPasswordHasher passwordHasher, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public Task<UserResponse> GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            UserResponse userResponse = new UserResponse();
            if (user == null)
            {
                userResponse = null;
            }
            else
            {
                userResponse.Id = user.Id;
                userResponse.Email = email;
                userResponse.Username = user.Username;
            }
            return Task.FromResult(userResponse);
        }
        public Task<UserResponse> GetByUserName(string username)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username);
            UserResponse userResponse = new UserResponse();
            if (user == null)
            {
                userResponse = null;
            }
            else
            {
                userResponse.Id = user.Id;
                userResponse.Email = username;
                userResponse.Username = user.Username;
            }
            
            return Task.FromResult(userResponse);
        }
        public bool CheckLogin(LoginRequest loginRequest)
        {
            var check = _context.Users.Where(c => c.Username == loginRequest.Username).FirstOrDefault();
            return _passwordHasher.VerifyPassword(loginRequest.Password, check.PasswordHash);
        }
        public Task SignUp(RegisterRequest registerRequest)
        {
            BcryptPasswordHasher passwordHasher = new BcryptPasswordHasher();
            string passwordHash = passwordHasher.HashPassword(registerRequest.Password);
            var item = _mapper.Map<RegisterRequest, User>(registerRequest);
            item.PasswordHash = passwordHash;
            _context.Users.Add(item);
            _context.SaveChanges();
            return Task.FromResult(item);
        }
    }
}
