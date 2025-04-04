using BrabantCareWebApi.Interfaces;
using BrabantCareWebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPITestsBrabant
{
    [TestClass]
    public sealed class UserRepositoryTests
    {
        private Mock<IUserRepository<User>> _mockRepository;
        private User _testUser;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IUserRepository<User>>();
            _testUser = new User
            {
                Id = "user123",
                Email = "user123@example.com"
            };
        }

        [TestMethod]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User> { _testUser };
            _mockRepository.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _mockRepository.Object.GetAllUsersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_testUser.Id, result[0].Id);
        }

        [TestMethod]
        public async Task DeleteUserAsync_ShouldDeleteUser()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteUserAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.DeleteUserAsync(_testUser.Id);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteUserAsync(It.Is<string>(id => id == _testUser.Id)), Times.Once);
        }
    }
}


