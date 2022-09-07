using ToDo.DTO;
using ToDo.DTOs;
using ToDo.Model;
using ToDo.Services.PasswordHash;

namespace ToDo.Services.User
{
    public class UserService : IUserService
    {
        private readonly ToDoDbContext _context;
        private readonly List<UserResponse> _users;
        public UserService(ToDoDbContext context)
        {
            _context = context;
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
                    PasswordHash = x.PasswordHash
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
        public Task SignUp(RegisterRequest registerRequest)
        {
            BcryptPasswordHasher passwordHasher = new BcryptPasswordHasher();
            string passwordHash = passwordHasher.HashPassword(registerRequest.Password);
            Model.User registrationUser = new Model.User()
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
