using BrabantCareWebApi.Interfaces;
using BrabantCareWebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPITestsBrabant
{
    [TestClass]
    public sealed class GuardianRepositoryTests
    {
        private Mock<IGuardianRepository<Guardian>> _mockRepository;
        private Guardian _testGuardian;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IGuardianRepository<Guardian>>();
            _testGuardian = new Guardian
            {
                ID = Guid.NewGuid(),
                UserID = "user123",
                FirstName = "Jan",
                LastName = "Janssen"
            };
        }

        [TestMethod]
        public async Task InsertAsync_ShouldReturnInsertedGuardian()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.InsertAsync(It.IsAny<Guardian>())).ReturnsAsync(_testGuardian);

            // Act
            var result = await _mockRepository.Object.InsertAsync(_testGuardian);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testGuardian.ID, result.ID);
            Assert.AreEqual(_testGuardian.UserID, result.UserID);
            Assert.AreEqual(_testGuardian.FirstName, result.FirstName);
            Assert.AreEqual(_testGuardian.LastName, result.LastName);
        }

        [TestMethod]
        public async Task ReadAsync_ById_ShouldReturnGuardian()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.ReadAsync(It.IsAny<Guid>())).ReturnsAsync(_testGuardian);

            // Act
            var result = await _mockRepository.Object.ReadAsync(_testGuardian.ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testGuardian.ID, result.ID);
            Assert.AreEqual(_testGuardian.UserID, result.UserID);
            Assert.AreEqual(_testGuardian.FirstName, result.FirstName);
            Assert.AreEqual(_testGuardian.LastName, result.LastName);
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReturnAllGuardians()
        {
            // Arrange
            var guardians = new List<Guardian> { _testGuardian };
            _mockRepository.Setup(repo => repo.ReadAsync()).ReturnsAsync(guardians);

            // Act
            var result = await _mockRepository.Object.ReadAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<Guardian>)result).Count);
            Assert.AreEqual(_testGuardian.ID, ((List<Guardian>)result)[0].ID);
        }

        [TestMethod]
        public async Task ReadByUserAsync_ShouldReturnGuardiansByUserId()
        {
            // Arrange
            var guardians = new List<Guardian> { _testGuardian };
            _mockRepository.Setup(repo => repo.ReadByUserAsync(It.IsAny<string>())).ReturnsAsync(guardians);

            // Act
            var result = await _mockRepository.Object.ReadByUserAsync(_testGuardian.UserID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<Guardian>)result).Count);
            Assert.AreEqual(_testGuardian.UserID, ((List<Guardian>)result)[0].UserID);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateGuardian()
        {
            // Arrange
            _testGuardian.FirstName = "Jane";
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Guardian>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.UpdateAsync(_testGuardian);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Guardian>(g => g.FirstName == "Jane")), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteGuardian()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.DeleteAsync(_testGuardian.ID);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(It.Is<Guid>(id => id == _testGuardian.ID)), Times.Once);
        }
    }
}
