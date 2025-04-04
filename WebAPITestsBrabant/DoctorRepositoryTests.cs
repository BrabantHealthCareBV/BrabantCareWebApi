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
    public sealed class DoctorRepositoryTests
    {
        private Mock<IDoctorRepository<Doctor>> _mockRepository;
        private Doctor _testDoctor;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDoctorRepository<Doctor>>();
            _testDoctor = new Doctor
            {
                ID = Guid.NewGuid(),
                Name = "Dr. John Doe",
                Specialization = "Cardiology"
            };
        }

        [TestMethod]
        public async Task InsertAsync_ShouldReturnInsertedDoctor()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.InsertAsync(It.IsAny<Doctor>())).ReturnsAsync(_testDoctor);

            // Act
            var result = await _mockRepository.Object.InsertAsync(_testDoctor);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testDoctor.ID, result.ID);
            Assert.AreEqual(_testDoctor.Name, result.Name);
            Assert.AreEqual(_testDoctor.Specialization, result.Specialization);
        }

        [TestMethod]
        public async Task ReadAsync_ById_ShouldReturnDoctor()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.ReadAsync(It.IsAny<Guid>())).ReturnsAsync(_testDoctor);

            // Act
            var result = await _mockRepository.Object.ReadAsync(_testDoctor.ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testDoctor.ID, result.ID);
            Assert.AreEqual(_testDoctor.Name, result.Name);
            Assert.AreEqual(_testDoctor.Specialization, result.Specialization);
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReturnAllDoctors()
        {
            // Arrange
            var doctors = new List<Doctor> { _testDoctor };
            _mockRepository.Setup(repo => repo.ReadAsync()).ReturnsAsync(doctors);

            // Act
            var result = await _mockRepository.Object.ReadAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<Doctor>)result).Count);
            Assert.AreEqual(_testDoctor.ID, ((List<Doctor>)result)[0].ID);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateDoctor()
        {
            // Arrange
            _testDoctor.Name = "Dr. Jane Doe";
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Doctor>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.UpdateAsync(_testDoctor);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Doctor>(d => d.Name == "Dr. Jane Doe")), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteDoctor()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.DeleteAsync(_testDoctor.ID);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(It.Is<Guid>(id => id == _testDoctor.ID)), Times.Once);
        }
    }
}
