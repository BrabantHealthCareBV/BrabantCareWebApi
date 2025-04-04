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
    public sealed class PatientRepositoryTests
    {
        private Mock<IPatientRepository<Patient>> _mockRepository;
        private Patient _testPatient;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IPatientRepository<Patient>>();
            _testPatient = new Patient
            {
                ID = Guid.NewGuid(),
                UserID = "user123",
                firstName = "Jan",
                lastName = "Janssen",
                birthdate = new DateTime(1992, 07, 21),
                nextAppointmentDate = new DateTime(2025, 04, 03),
                guardianID = Guid.NewGuid(),
                treatmentPlanID = Guid.NewGuid(),
                doctorID = Guid.NewGuid(),
                gameState = 1,
                score = 46
            };
        }

        [TestMethod]
        public async Task InsertAsync_ShouldReturnInsertedPatient()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.InsertAsync(It.IsAny<Patient>())).ReturnsAsync(_testPatient);

            // Act
            var result = await _mockRepository.Object.InsertAsync(_testPatient);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testPatient.ID, result.ID);
            Assert.AreEqual(_testPatient.UserID, result.UserID);
            Assert.AreEqual(_testPatient.firstName, result.firstName);
            Assert.AreEqual(_testPatient.lastName, result.lastName);
        }

        [TestMethod]
        public async Task ReadAsync_ById_ShouldReturnPatient()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.ReadAsync(It.IsAny<Guid>())).ReturnsAsync(_testPatient);

            // Act
            var result = await _mockRepository.Object.ReadAsync(_testPatient.ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testPatient.ID, result.ID);
            Assert.AreEqual(_testPatient.UserID, result.UserID);
            Assert.AreEqual(_testPatient.firstName, result.firstName);
            Assert.AreEqual(_testPatient.lastName, result.lastName);
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReturnAllPatients()
        {
            // Arrange
            var patients = new List<Patient> { _testPatient };
            _mockRepository.Setup(repo => repo.ReadAsync()).ReturnsAsync(patients);

            // Act
            var result = await _mockRepository.Object.ReadAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<Patient>)result).Count);
            Assert.AreEqual(_testPatient.ID, ((List<Patient>)result)[0].ID);
        }

        [TestMethod]
        public async Task ReadByUserAsync_ShouldReturnPatientsByUserId()
        {
            // Arrange
            var patients = new List<Patient> { _testPatient };
            _mockRepository.Setup(repo => repo.ReadByUserAsync(It.IsAny<string>())).ReturnsAsync(patients);

            // Act
            var result = await _mockRepository.Object.ReadByUserAsync(_testPatient.UserID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<Patient>)result).Count);
            Assert.AreEqual(_testPatient.UserID, ((List<Patient>)result)[0].UserID);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdatePatient()
        {
            // Arrange
            _testPatient.firstName = "Jane";
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Patient>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.UpdateAsync(_testPatient);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<Patient>(p => p.firstName == "Jane")), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeletePatient()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.DeleteAsync(_testPatient.ID);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(It.Is<Guid>(id => id == _testPatient.ID)), Times.Once);
        }
    }
}

