using Xunit;
using Moq;
using AuthService.Application.Services;
using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AuthService.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task GenerateTokenAsync_ReturnsTokenResultDTO()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            var service = new AuthService.Application.Services.AuthService(config);
            var user = new JWTUserDTO { Username = "test", Id = Guid.NewGuid(), Roles = new System.Collections.Generic.List<string> { "admin" } };

            // Act
            var result = await service.GenerateTokenAsync(user);

            // Assert
            Assert.NotNull(result);
            // Add more asserts as needed for token structure
        }
    }

    public class LoginAppServiceTests
    {
        

        [Fact]
        public async Task AuthenticateAsync_InvalidUser_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            var userRepoMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var loginDto = new LoginDTO { Username = "test", Password = "wrong" };
            userRepoMock.Setup(r => r.GetUserByUsernameAsync("test")).ReturnsAsync((JWTUser?)null);
            var service = new LoginAppService(authServiceMock.Object, userRepoMock.Object, mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.AuthenticateAsync(loginDto));
        }
    }
}
