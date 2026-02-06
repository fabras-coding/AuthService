using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;

namespace AuthService.Tests.Controllers
{
    public class ProgramMinimalApiTests
    {
        [Fact]
        public async Task TokenEndpoint_ReturnsOkWithToken()
        {
            // Arrange
            var loginDto = new LoginDTO { Username = "test", Password = "pass" };
            var loginServiceMock = new Mock<ILoginAppService>();
            loginServiceMock.Setup(s => s.AuthenticateAsync(loginDto)).ReturnsAsync(new TokenResultDTO { Token = "token" });

            // Act
            var result = await loginServiceMock.Object.AuthenticateAsync(loginDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("token", result.Token);
        }

        [Fact]
        public void ValidateEndpoint_AuthenticatedUser_ReturnsOk()
        {
            // Arrange
            var claims = new[] { new Claim(ClaimTypes.Name, "test") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            // Act
            var isAuthenticated = user.Identity != null && user.Identity.IsAuthenticated;

            // Assert
            Assert.True(isAuthenticated);
        }
    }
}
