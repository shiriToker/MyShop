using Entity;
using Repository;
using Zxcvbn;

namespace Services
{
    public class MyService : IMyService
    {
        IMyRepository repository;

        public MyService(IMyRepository myRepository)
        {
            repository = myRepository;

        }

        public Task<User> getById(int id)
        {
            return repository.getById(id);

        }
        public Task<User> LogIn(string Password, string UserName)
        {
            return repository.LogIn(Password, UserName);
        }

        public void updateUser(int id, User userToUpdate)//return user
        {
            if (Password(userToUpdate.Password) < 3)
            {
                throw new Exception("סיסמה חלשה מדי");
            }
            repository.updateUser(id, userToUpdate);

        }
        public async Task<User> createUser(User user)
        {
            return Password(user.Password) < 3 ? null : await repository.createUser(user);
        }
        public int Password(string password)
        {

            var result = Zxcvbn.Core.EvaluatePassword(password);
            return (result.Score);

        }







    }
}
