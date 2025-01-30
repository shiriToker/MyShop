using Entity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class IntegrationTest :IClassFixture<DatabaseFixture>
    {

        private readonly MyShop328306782Context _context;

        public IntegrationTest(DatabaseFixture fixture)
        {
            _context = fixture.Context; 
        }

        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            var user = new User { UserName = "shirit782@gmail.com", Password = "Student@264" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var retrievedUser = await _context.Users.FindAsync(user.UserId);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.UserName, retrievedUser.UserName);
        }


    }
}