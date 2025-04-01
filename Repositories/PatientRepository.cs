using Dapper;
using BrabantCareWebApi.Models;
using Microsoft.Data.SqlClient;

namespace BrabantCareWebApi.Repositories
{
    public class PatientRepository
    {
        private readonly string sqlConnectionString;

        public PatientRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Patient> InsertAsync(Patient patient)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(@"
                    INSERT INTO [Patients] (Id, FirstName, LastName, Birthdate, NextAppointmentDate, GuardianId, TreatmentPlanId, DoctorID) 
                    VALUES (@Id, @FirstName, @LastName, @Birthdate, @NextAppointmentDate, @GuardianId, @TreatmentPlanId, @DoctorID)", patient);
                return patient;
            }
        }

        public async Task<Patient?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Patient>(
                    "SELECT * FROM [Patients] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Patient>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Patient>("SELECT * FROM [Patients]");
            }
        }

        public async Task UpdateAsync(Patient patient)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(@"
                    UPDATE [Patients] SET 
                    FirstName = @FirstName, 
                    LastName = @LastName, 
                    Birthdate = @Birthdate, 
                    NextAppointmentDate = @NextAppointmentDate, 
                    GuardianId = @GuardianId, 
                    TreatmentPlanId = @TreatmentPlanId, 
                    DoctorID = @DoctorID 
                    WHERE Id = @Id", patient);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Patients] WHERE Id = @Id", new { id });
            }
        }
    }
}
