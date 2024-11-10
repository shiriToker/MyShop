using Entity;

namespace Repository
{
    public interface IMyRepository
    {
        User createUser(User user);
        User getById(int id);
        User LogIn(string Password, string UserName);
        void updateUser(int id, User userToUpdate);
    }
}