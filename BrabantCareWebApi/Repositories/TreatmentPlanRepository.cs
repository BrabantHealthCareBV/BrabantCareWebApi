using BrabantCareWebApi.Interfaces;
using BrabantCareWebApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Numerics;

namespace BrabantCareWebApi.Repositories
{
    public class TreatmentPlanRepository : ITreatmentPlanRepository<TreatmentPlan>
    {
        private readonly string sqlConnectionString;

        private readonly ILogger<TreatmentPlanRepository> _logger;
        public TreatmentPlanRepository(string sqlConnectionString, ILogger<TreatmentPlanRepository> logger)
        {
            this.sqlConnectionString = sqlConnectionString;
            _logger = logger;
        }

        public async Task<TreatmentPlan> InsertAsync(TreatmentPlan treatmentPlan)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    Console.WriteLine($"Id: {treatmentPlan.ID}, Name: {treatmentPlan.Name}");

                    await sqlConnection.ExecuteAsync(
                        "INSERT INTO [TreatmentPlans] (Id, Name) VALUES (@Id, @Name)", treatmentPlan);
                    return treatmentPlan;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting treatmentplan: {treatmentPlanName}", treatmentPlan.Name);
                throw;
            }
        }

        public async Task<TreatmentPlan?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<TreatmentPlan>(
                    "SELECT * FROM [TreatmentPlans] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<TreatmentPlan>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<TreatmentPlan>("SELECT * FROM [TreatmentPlans]");
            }
        }

        public async Task UpdateAsync(TreatmentPlan treatmentPlan)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "UPDATE [TreatmentPlans] SET Name = @Name WHERE Id = @Id", treatmentPlan);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [TreatmentPlans] WHERE Id = @Id", new { id });
            }
        }
    }
}
