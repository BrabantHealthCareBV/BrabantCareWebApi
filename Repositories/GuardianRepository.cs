using BrabantCareWebApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BrabantCareWebApi.Repositories
{
    public class GuardianRepository
    {
        private readonly string sqlConnectionString;

        public GuardianRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Guardian> InsertAsync(Guardian guardian)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "INSERT INTO [Guardians] (Id, FirstName, LastName) VALUES (@Id, @FirstName, @LastName)", guardian);
                return guardian;
            }
        }

        public async Task<Guardian?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Guardian>(
                    "SELECT * FROM [Guardians] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Guardian>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Guardian>("SELECT * FROM [Guardians]");
            }
        }

        public async Task UpdateAsync(Guardian guardian)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "UPDATE [Guardians] SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id", guardian);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Guardians] WHERE Id = @Id", new { id });
            }
        }
    }
}
