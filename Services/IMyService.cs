using Entity;

namespace Services
{
    public interface IMyService
    {
        User createUser(User user);
        User getById(int id);
        User LogIn(string Password, string UserName);
        int Password(string password);
        void updateUser(int id, User userToUpdate);
    }
}