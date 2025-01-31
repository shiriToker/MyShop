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
        public async Task LogIn_ValidCredentials_ReturnsUser()
        {
            var user = new User { UserName = "shirit782@gmail.com", Password = "Student@264" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var userRepository = new MyRepository(_context);


            // Act
            var retrievedUser = await userRepository.LogIn(user.Password,user.UserName);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.UserName, retrievedUser.UserName);
        }

        [Fact]
        public async Task LogIn_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var userRepository = new MyRepository(_context);

            // Act
            var result = await userRepository.LogIn("wrongPassword", "nonExistentUser");

            // Assert
            Assert.Null(result);  // המשתמש לא קיים, אז הפונקציה מחזירה null
        }

        [Fact]
        public async Task LogIn_WrongPassword_ReturnsNull()
        {
            // Arrange
            var user = new User { UserName = "ShiriToker", Password = "shirit782@gmail.com" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userRepository = new MyRepository(_context);

            // Act
            var result = await userRepository.LogIn("wrongPassword", user.UserName); // סיסמה שגויה

            // Assert
            Assert.Null(result);  // הפונקציה צריכה להחזיר null בגלל שסיסמה לא נכונה
        }




    }
}