using Microsoft.AspNetCore.Mvc;
using Moq;
using RentalyaAPI.Controllers;
using RentalyaAPI.Models;
using RentalyaAPI.Services;
using Xunit;

namespace RentalyaAPI.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Register_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var model = new RegisterModel
            {
                FirstName = "Test",
                LastName = "Kullanıcı",
                Email = "test@example.com",
                Password = "123456",
                PhoneNumber = "5551234567"
            };

            _mockAuthService.Setup(x => x.RegisterUser(It.IsAny<RegisterModel>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic value = okResult.Value;
            Assert.Equal("Kullanıcı başarıyla kaydedildi", (string)value.message);
        }
    }
} 