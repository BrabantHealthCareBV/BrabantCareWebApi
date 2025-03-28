using BrabantCareWebApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BrabantCareWebApi.Repositories
{
    public class CareMomentRepository
    {
        private readonly string sqlConnectionString;

        public CareMomentRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<CareMoment> InsertAsync(CareMoment careMoment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "INSERT INTO [CareMoments] (Id, Name, Url, Image, DurationInMinutes) VALUES (@Id, @Name, @Url, @Image, @DurationInMinutes)", careMoment);
                return careMoment;
            }
        }

        public async Task<CareMoment?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<CareMoment>(
                    "SELECT * FROM [CareMoments] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<CareMoment>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<CareMoment>("SELECT * FROM [CareMoments]");
            }
        }

        public async Task UpdateAsync(CareMoment careMoment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "UPDATE [CareMoments] SET Name = @Name, Url = @Url, Image = @Image, DurationInMinutes = @DurationInMinutes WHERE Id = @Id", careMoment);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [CareMoments] WHERE Id = @Id", new { id });
            }
        }
    }
}
