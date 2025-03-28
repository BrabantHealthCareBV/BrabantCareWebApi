using BrabantCareWebApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BrabantCareWebApi.Repositories
{
    public class TreatmentPlanRepository
    {
        private readonly string sqlConnectionString;

        public TreatmentPlanRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<TreatmentPlan> InsertAsync(TreatmentPlan treatmentPlan)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "INSERT INTO [TreatmentPlans] (Id, Name) VALUES (@Id, @Name)", treatmentPlan);
                return treatmentPlan;
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
