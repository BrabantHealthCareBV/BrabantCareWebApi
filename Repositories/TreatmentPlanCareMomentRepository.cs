using Dapper;
using Microsoft.Data.SqlClient;
using BrabantCareWebApi.Models;
using Microsoft.Extensions.Logging;

namespace BrabantCareWebApi.Repositories
{
    public class TreatmentPlanCareMomentRepository
    {
        private readonly string sqlConnectionString;
        private readonly ILogger<TreatmentPlanCareMomentRepository> _logger;

        public TreatmentPlanCareMomentRepository(string sqlConnectionString, ILogger<TreatmentPlanCareMomentRepository> logger)
        {
            this.sqlConnectionString = sqlConnectionString;
            _logger = logger;
        }

        public async Task InsertAsync(TreatmentPlanCareMoment entity)
        {
            try
            {
                _logger.LogInformation("Inserting TreatmentPlanCareMoment: {TreatmentPlanId}, {CareMomentId}", entity.TreatmentPlanID);

                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync(
                        "INSERT INTO TreatmentPlan_CareMoments (TreatmentPlanID, CareMomentID, [Order]) VALUES (@TreatmentPlanID, @CareMomentID, @Order)", entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting TreatmentPlanCareMoment: {TreatmentPlanId}, {CareMomentId}", entity.TreatmentPlanID, entity.CareMomentID);
                throw;
            }
        }

        public async Task<IEnumerable<TreatmentPlanCareMoment>> ReadAsync(Guid treatmentPlanId)
        {
            try
            {
                _logger.LogInformation("Fetching TreatmentPlanCareMoments for TreatmentPlanID: {TreatmentPlanId}", treatmentPlanId);

                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    return await sqlConnection.QueryAsync<TreatmentPlanCareMoment>(
                        "SELECT * FROM TreatmentPlan_CareMoments WHERE TreatmentPlanID = @TreatmentPlanID ORDER BY [Order]",
                        new { TreatmentPlanID = treatmentPlanId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching TreatmentPlanCareMoments for TreatmentPlanID: {TreatmentPlanId}", treatmentPlanId);
                throw;
            }
        }

        public async Task UpdateAsync(TreatmentPlanCareMoment entity)
        {
            try
            {
                _logger.LogInformation("Updating TreatmentPlanCareMoment: {TreatmentPlanId}, {CareMomentId}", entity.TreatmentPlanID, entity.CareMomentID);

                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync(
                        "UPDATE TreatmentPlan_CareMoments SET [Order] = @Order WHERE TreatmentPlanID = @TreatmentPlanID AND CareMomentID = @CareMomentID",
                        entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating TreatmentPlanCareMoment: {TreatmentPlanId}, {CareMomentId}", entity.TreatmentPlanID, entity.CareMomentID);
                throw;
            }
        }

        public async Task DeleteAsync(Guid treatmentPlanId, Guid careMomentId)
        {
            try
            {
                _logger.LogInformation("Deleting TreatmentPlanCareMoment: {TreatmentPlanId}, {CareMomentId}", treatmentPlanId, careMomentId);

                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync(
                        "DELETE FROM TreatmentPlan_CareMoments WHERE TreatmentPlanID = @TreatmentPlanID AND CareMomentID = @CareMomentID",
                        new { TreatmentPlanID = treatmentPlanId, CareMomentID = careMomentId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting TreatmentPlanCareMoment: {TreatmentPlanId}, {CareMomentId}", treatmentPlanId, careMomentId);
                throw;
            }
        }
    }
}
