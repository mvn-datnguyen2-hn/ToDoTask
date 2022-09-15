
using ToDo.DTOs;
using ToDo.Models;
using ToDo.Services.PasswordHash;

namespace ToDo.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ToDoDbContext _context;
        private readonly List<UserResponse> _users;
        private readonly BcryptPasswordHasher _passwordHasher;
        public UserService(ToDoDbContext context, BcryptPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _users = new List<UserResponse>();
            GetUser();
        }
        public  List<UserResponse> GetUser()
        {
            var user = _context.Users.ToList();
            foreach (var x in user)
            {
                UserResponse userResponse = new UserResponse
                {
                    Id = x.Id,
                    Email = x.Email,
                    Username = x.Username,
                };
                _users.Add(userResponse);
            }
            return _users;
        }
        public Task<UserResponse> GetByEmail(string email)
        {
            return Task.FromResult(_users.FirstOrDefault(c => c.Email == email));
        }
        public Task<UserResponse> GetByUserName(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(c => c.Username == username));
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
            Models.User registrationUser = new Models.User()
            {
                Email = registerRequest.Email,
                Username = registerRequest.Username,
                PasswordHash = passwordHash
            };
            _context.Users.Add(registrationUser);
            _context.SaveChanges();
            return Task.FromResult(registrationUser);
        }
    }
}
