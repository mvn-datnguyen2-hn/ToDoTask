using ToDo.DTO;
using ToDo.Model;
using ToDo.Services.PasswordHash;

namespace ToDo.Services.User
{
    public class UserService : IUserService
    {
        private readonly ToDoDbContext _context;
        private List<Model.User> _users;
        public UserService(ToDoDbContext context)
        {
            _context = context;
            _users = GetUser();
        }
       
        public  List<Model.User> GetUser()
        {
            _users = _context.Users.ToList();
            return _users;
        }

        public Task<Model.User> GetByEmail(string email)
        {
            return Task.FromResult(_users.FirstOrDefault(c => c.Email == email));
        }
        public Task<Model.User> GetByUserName(string username)
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
