using Entity;
using Repository;
using Services;

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
            Assert.Null(result);  
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
            var result = await userRepository.LogIn("wrongPassword", user.UserName); 

            // Assert
            Assert.Null(result);  
        }

        [Fact]
        public async Task CreateOrder_checkOrderSum_ReturnsOrder()
        {
            // Arrange
            var products = new List<Product> { new Product { Price = 6, ProductName="eggs", CaregoryId=1, Caregory=new()
            {CategoryName="basic"} ,Description="beautiful eggs",ImgUrl="6.jpeg" }
          };
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            var orderItems = new List<OrderItem>() { new() { ProductId = 1 } };
            var order = new Order { OrderSum = 6, OrderItems = orderItems };
            var orderRepository = new OrderRepository(_context);
            var productRepository = new ProductRepository(_context);
            var orderService = new OrderService(orderRepository, productRepository);

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
            var products = new List<Product> { new Product { Price = 6, ProductName="eggs", CaregoryId=1, Caregory=new()
            {CategoryName="basic"} ,Description="beautiful eggs",ImgUrl="6.jpeg" }
          };

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            var orderItems = new List<OrderItem>() { new() { ProductId = 1 } };
            var order = new Order { OrderSum = 3, OrderItems = orderItems };
            var orderRepository = new OrderRepository(_context);
            var productRepository = new ProductRepository(_context);
            var orderService = new OrderService(orderRepository, productRepository);

            // Act
            var result = await orderService.createOrder(order);

            // Assert
            Assert.Null(result);
        }
    }
}