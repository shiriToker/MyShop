using Entity;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            var user = new User { UserName = "ShiriToker", Password = "shirit782@gmail.com" };
           var mocContext = new Mock<MyShop328306782Context>();
            var users = new List<User>() { user };
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new MyRepository(mocContext.Object);

            var result = await userRepository.LogIn( user.Password,user.UserName);

            Assert.Equal(user, result);
        }
    }
}