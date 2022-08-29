using ToDo.Model;

namespace ToDo.Services.User
{
    public class UserRespository : IUserRespository
    {
        private readonly List<Model.User> _users = new List<Model.User>();
        public Task<Model.User> GetByEmail(string email)
        {
            return Task.FromResult(_users.FirstOrDefault(c => c.Email == email));
        }
        public Task<Model.User> GetByUserName(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(c => c.Username == username));
        }
        public Task<Model.User> CreateUser(Model.User user)
        {
            user.Id = Guid.NewGuid();
            _users.Add(user);
            return Task.FromResult(user);
        }
    }
}
