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

        public async Task<User> getById(int id)
        {
            return await repository.getById(id);

        }
        public async Task<User> LogIn(string Password, string UserName)
        {
            return await repository.LogIn(Password, UserName);
        }

        public async Task<User> updateUser(int id, User userToUpdate)
        {
            if (Password(userToUpdate.Password) < 3)
            {
                throw new Exception("סיסמה חלשה מדי");
            }
          return await repository.updateUser(id, userToUpdate);

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
