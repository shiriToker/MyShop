﻿using Entity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Linq;
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
            User user = await _dbcontext.Users.FindAsync(id);
            return user == null ? null : user;

        }
        public async Task<User> createUser(User user)
        {
            await _dbcontext.Users.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
            return user;

        }
        public async Task updateUser(int id, User userToUpdate)//return user
        {
            userToUpdate.UserId = id;
            _dbcontext.Update(userToUpdate);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<User> LogIn(string Password, string UserName)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(user => user.UserName == UserName && user.Password == Password);
        }
    }
}
