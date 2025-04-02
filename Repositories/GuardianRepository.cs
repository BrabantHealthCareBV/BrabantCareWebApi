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
            guardian.ID = Guid.NewGuid();
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var existingGuardian = await sqlConnection.QuerySingleOrDefaultAsync<Guardian>(@"
                    SELECT * FROM [Guardians] 
                    WHERE UserID = @UserID AND FirstName = @FirstName AND LastName = @LastName",
                    new { guardian.UserID, guardian.FirstName, guardian.LastName });

                if (existingGuardian != null)
                {
                    return null; // Guardian already exists
                }

                await sqlConnection.ExecuteAsync(@"
                    INSERT INTO [Guardians] (ID, UserID, FirstName, LastName)
                    VALUES (@ID, @UserID, @FirstName, @LastName)", guardian);

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

        public async Task<IEnumerable<Guardian>> ReadByUserAsync(string userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {

                var guardians = await sqlConnection.QueryAsync<Guardian>("SELECT * FROM [Guardians] WHERE UserId = @UserId", new { UserId = userId });

                return guardians;
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
