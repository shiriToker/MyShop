using Entity;

namespace Repository
{
    public interface IMyRepository
    {
        Task<User> createUser(User user);
        Task<User> getById(int id);
        Task<User> LogIn(string Password, string UserName);
        Task updateUser(int id, User userToUpdate);
    }
}