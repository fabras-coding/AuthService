using Xunit;
using Moq;
using AuthService.Infra.Data.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuthService.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task AddAsync_AddsUserToDb()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "AddUserTestDb").Options;
            var user = new JWTUser { Username = "test", Password = "pass" };
            using (var context = new AuthDbContext(options))
            {
                var repo = new UserRepository(context);
                // Act
                await repo.AddAsync(user);
            }
            // Assert
            using (var context = new AuthDbContext(options))
            {
                Assert.Single(context.JWTUsers);
                Assert.Equal("test", context.JWTUsers.First().Username);
            }
        }

        [Fact]
        public async Task GetUserByUsernameAsync_ReturnsUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "GetUserTestDb").Options;
            var user = new JWTUser { Username = "test", Password = "pass" };
            using (var context = new AuthDbContext(options))
            {
                context.JWTUsers.Add(user);
                context.SaveChanges();
            }
            using (var context = new AuthDbContext(options))
            {
                var repo = new UserRepository(context);
                // Act
                var result = await repo.GetUserByUsernameAsync("test");
                // Assert
                Assert.NotNull(result);
                Assert.Equal("test", result.Username);
            }
        }
    }
}
