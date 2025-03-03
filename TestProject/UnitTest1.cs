using Entity;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using Services;
namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task LogIn_ValidCredentials_ReturnsUser()
        {
            var user = new User { UserName = "ShiriToker", Password = "shirit782@gmail.com" };
           var mocContext = new Mock<MyShop328306782Context>();
            var users = new List<User>() { user };
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new MyRepository(mocContext.Object);

            var result = await userRepository.LogIn( user.Password,user.UserName);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task LogIn_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var mocContext = new Mock<MyShop328306782Context>();
            var users = new List<User>();
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new MyRepository(mocContext.Object);

            // Act
            var result = await userRepository.LogIn("wrongPassword", "nonExistentUser");

            // Assert
            Assert.Null(result); 
        }

        [Fact]
        public async Task LogIn_WrongPassword_ReturnsNull()
        {
            // Arrange
            var user = new User { UserName = "ShiriToker", Password = "shirit782@gmail.com" };
            var mocContext = new Mock<MyShop328306782Context>();
            var users = new List<User> { user };
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new MyRepository(mocContext.Object);

            // Act
            var result = await userRepository.LogIn("wrongPassword", user.UserName); 

            // Assert
            Assert.Null(result); 
        }

        [Fact]
        public async Task CreateOrder_checkOrderSum_ReturnsOrder()
        {
            // Arrange
            var orderItems = new List<OrderItem>() { new() { ProductId = 1 } };
            var order = new Order { OrderSum = 6, OrderItems = orderItems };

            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockProductRepository = new Mock<IProductRepository>();

            var products = new List<Product> { new Product { ProductId = 1, Price = 6 } };
            mockProductRepository.Setup(x => x.getAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?[]>()))
                                 .ReturnsAsync(products);

            mockOrderRepository.Setup(x => x.createOrder(It.IsAny<Order>()))
                               .ReturnsAsync(order);

            var orderService = new OrderService(mockOrderRepository.Object, mockProductRepository.Object);

            // Act
            var result = await orderService.createOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order, result);
        }

        [Fact]
        public async Task CreateOrder_checkOrderSum_ReturnsNull()
        {
            // Arrange
            var orderItems = new List<OrderItem>() { new() { ProductId = 1 } };
            var order = new Order { OrderSum = 3, OrderItems = orderItems };

            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockProductRepository = new Mock<IProductRepository>();

            var products = new List<Product> { new Product { ProductId = 1, Price = 6 } };
            mockProductRepository.Setup(x => x.getAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?[]>()))
                                 .ReturnsAsync(products);

            mockOrderRepository.Setup(x => x.createOrder(It.IsAny<Order>()))
                               .ReturnsAsync(order);

            var orderService = new OrderService(mockOrderRepository.Object, mockProductRepository.Object,null);

            // Act
            var result = await orderService.createOrder(order);

            // Assert
            Assert.Null(result);
        }



    }
}