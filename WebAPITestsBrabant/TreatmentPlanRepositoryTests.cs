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
    public sealed class TreatmentPlanRepositoryTests
    {
        private Mock<ITreatmentPlanRepository<TreatmentPlan>> _mockRepository;
        private TreatmentPlan _testTreatmentPlan;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ITreatmentPlanRepository<TreatmentPlan>>();
            _testTreatmentPlan = new TreatmentPlan
            {
                ID = Guid.NewGuid(),
                Name = "Route A",
                PatientIDs = new List<Guid> { Guid.NewGuid() },
                CareMomentIDs = new List<Guid> { Guid.NewGuid() }
            };
        }

        [TestMethod]
        public async Task InsertAsync_ShouldReturnInsertedTreatmentPlan()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.InsertAsync(It.IsAny<TreatmentPlan>())).ReturnsAsync(_testTreatmentPlan);

            // Act
            var result = await _mockRepository.Object.InsertAsync(_testTreatmentPlan);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testTreatmentPlan.ID, result.ID);
            Assert.AreEqual(_testTreatmentPlan.Name, result.Name);
        }

        [TestMethod]
        public async Task ReadAsync_ById_ShouldReturnTreatmentPlan()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.ReadAsync(It.IsAny<Guid>())).ReturnsAsync(_testTreatmentPlan);

            // Act
            var result = await _mockRepository.Object.ReadAsync(_testTreatmentPlan.ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testTreatmentPlan.ID, result.ID);
            Assert.AreEqual(_testTreatmentPlan.Name, result.Name);
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReturnAllTreatmentPlans()
        {
            // Arrange
            var treatmentPlans = new List<TreatmentPlan> { _testTreatmentPlan };
            _mockRepository.Setup(repo => repo.ReadAsync()).ReturnsAsync(treatmentPlans);

            // Act
            var result = await _mockRepository.Object.ReadAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<TreatmentPlan>)result).Count);
            Assert.AreEqual(_testTreatmentPlan.ID, ((List<TreatmentPlan>)result)[0].ID);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateTreatmentPlan()
        {
            // Arrange
            _testTreatmentPlan.Name = "Route B";
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<TreatmentPlan>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.UpdateAsync(_testTreatmentPlan);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<TreatmentPlan>(tp => tp.Name == "Route B")), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteTreatmentPlan()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.DeleteAsync(_testTreatmentPlan.ID);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(It.Is<Guid>(id => id == _testTreatmentPlan.ID)), Times.Once);
        }
    }
}


