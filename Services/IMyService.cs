using Entity;

namespace Services
{
    public interface IMyService
    {
        Task<User> createUser(User user);
        Task<User> getById(int id);
        Task<User> LogIn(string Password, string UserName);
        int Password(string password);
        Task<User> updateUser(int id, User userToUpdate);
    }
}