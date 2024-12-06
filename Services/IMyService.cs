using Entity;

namespace Services
{
    public interface IMyService
    {
        Task<User> createUser(User user);
        Task<User> getById(int id);
        Task<User> LogIn(string Password, string UserName);
        int Password(string password);
        void updateUser(int id, User userToUpdate);
    }
}