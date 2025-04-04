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
    public sealed class TreatmentPlanCareMomentRepositoryTests
    {
        private Mock<ITreatmentPlanCareMomentRepository<TreatmentPlanCareMoment>> _mockRepository;
        private TreatmentPlanCareMoment _testTreatmentPlanCareMoment;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ITreatmentPlanCareMomentRepository<TreatmentPlanCareMoment>>();
            _testTreatmentPlanCareMoment = new TreatmentPlanCareMoment
            {
                TreatmentPlanID = Guid.NewGuid(),
                CareMomentID = Guid.NewGuid(),
                Order = 1,
                CareMomentName = "Controle afspraak"
            };
        }

        [TestMethod]
        public async Task InsertAsync_ShouldInsertTreatmentPlanCareMoment()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.InsertAsync(It.IsAny<TreatmentPlanCareMoment>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.InsertAsync(_testTreatmentPlanCareMoment);

            // Assert
            _mockRepository.Verify(repo => repo.InsertAsync(It.Is<TreatmentPlanCareMoment>(tpcm => tpcm.TreatmentPlanID == _testTreatmentPlanCareMoment.TreatmentPlanID)), Times.Once);
        }

        [TestMethod]
        public async Task ReadAsync_ShouldReturnTreatmentPlanCareMoments()
        {
            // Arrange
            var treatmentPlanCareMoments = new List<TreatmentPlanCareMoment> { _testTreatmentPlanCareMoment };
            _mockRepository.Setup(repo => repo.ReadAsync(It.IsAny<Guid>())).ReturnsAsync(treatmentPlanCareMoments);

            // Act
            var result = await _mockRepository.Object.ReadAsync(_testTreatmentPlanCareMoment.TreatmentPlanID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<TreatmentPlanCareMoment>)result).Count);
            Assert.AreEqual(_testTreatmentPlanCareMoment.TreatmentPlanID, ((List<TreatmentPlanCareMoment>)result)[0].TreatmentPlanID);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateTreatmentPlanCareMoment()
        {
            // Arrange
            _testTreatmentPlanCareMoment.CareMomentName = "Gips behandeling";
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<TreatmentPlanCareMoment>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.UpdateAsync(_testTreatmentPlanCareMoment);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<TreatmentPlanCareMoment>(tpcm => tpcm.CareMomentName == "Gips behandeling")), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteTreatmentPlanCareMoment()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.DeleteAsync(_testTreatmentPlanCareMoment.TreatmentPlanID, _testTreatmentPlanCareMoment.CareMomentID);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(It.Is<Guid>(tpId => tpId == _testTreatmentPlanCareMoment.TreatmentPlanID), It.Is<Guid>(cmId => cmId == _testTreatmentPlanCareMoment.CareMomentID)), Times.Once);
        }
    }
}

