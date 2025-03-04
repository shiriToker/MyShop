using Entity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Linq;
using Microsoft.Extensions.Logging;
using Repository.Exceptions;

namespace Repository
   
{
    public class MyRepository : IMyRepository
    {
        MyShop328306782Context _dbcontext;
        public MyRepository(MyShop328306782Context context)
        {
            _dbcontext = context;

        }

        public async Task<User> getById(int id)
        {
            User user = await _dbcontext.Users.Include(user => user.Orders).FirstOrDefaultAsync(user=>user.UserId==id);
            return user;

        }
        public async Task<User> createUser(User user)
        {
            var existingUser = await _dbcontext.Users
            .FirstOrDefaultAsync(u => u.UserName == user.UserName);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("user with the same userName already exist");
            }

            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
            return user;

        }
        public async Task<User> updateUser(int id, User userToUpdate)
        {
           var existingUser = await _dbcontext.Users
          .FirstOrDefaultAsync(u => u.UserName == userToUpdate.UserName);
            if (existingUser != null)
            {
                _dbcontext.Entry(existingUser).State = EntityState.Detached;
            }
            if (existingUser != null && existingUser.UserId!=id)
            {
                throw new UserAlreadyExistsException("user with the same userName already exist");
            }

            userToUpdate.UserId = id;
             _dbcontext.Users.Update(userToUpdate);
            await _dbcontext.SaveChangesAsync();
            return userToUpdate;
        }

        public async Task<User> LogIn(string Password, string UserName)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(user => user.UserName == UserName && user.Password == Password);
        }
    }
}
