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
    public sealed class CareMomentRepositoryTests
    {
        private Mock<ICareMomentRepository<CareMoment>> _mockRepository;
        private CareMoment _testCareMoment;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ICareMomentRepository<CareMoment>>();
            _testCareMoment = new CareMoment
            {
                ID = Guid.NewGuid(),
                Name = "Controle afspraak",
                Url = null,
                Image = null,
                DurationInMinutes = 30
            };
        }

        [TestMethod]
        public async Task InsertAsync_ShouldReturnInsertedCareMoment()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.InsertAsync(It.IsAny<CareMoment>())).ReturnsAsync(_testCareMoment);

            // Act
            var result = await _mockRepository.Object.InsertAsync(_testCareMoment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testCareMoment.ID, result.ID);
            Assert.AreEqual(_testCareMoment.Name, result.Name);
            Assert.AreEqual(_testCareMoment.Url, result.Url);
            Assert.AreEqual(_testCareMoment.DurationInMinutes, result.DurationInMinutes);
        }

        [TestMethod]
        public async Task ReadAsync_ById_ShouldReturnCareMoment()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.ReadAsync(It.IsAny<Guid>())).ReturnsAsync(_testCareMoment);

            // Act
            var result = await _mockRepository.Object.ReadAsync(_testCareMoment.ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testCareMoment.ID, result.ID);
            Assert.AreEqual(_testCareMoment.Name, result.Name);
            Assert.AreEqual(_testCareMoment.Url, result.Url);
            Assert.AreEqual(_testCareMoment.DurationInMinutes, result.DurationInMinutes);
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReturnAllCareMoments()
        {
            // Arrange
            var careMoments = new List<CareMoment> { _testCareMoment };
            _mockRepository.Setup(repo => repo.ReadAsync()).ReturnsAsync(careMoments);

            // Act
            var result = await _mockRepository.Object.ReadAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<CareMoment>)result).Count);
            Assert.AreEqual(_testCareMoment.ID, ((List<CareMoment>)result)[0].ID);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateCareMoment()
        {
            // Arrange
            _testCareMoment.Name = "Evening Routine";
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<CareMoment>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.UpdateAsync(_testCareMoment);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<CareMoment>(cm => cm.Name == "Evening Routine")), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteCareMoment()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.DeleteAsync(_testCareMoment.ID);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(It.Is<Guid>(id => id == _testCareMoment.ID)), Times.Once);
        }
    }
}
