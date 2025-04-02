using Dapper;
using BrabantCareWebApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace BrabantCareWebApi.Repositories
{
    public class PatientRepository
    {
        private readonly string sqlConnectionString;
        private readonly ILogger<PatientRepository> logger;

        public PatientRepository(string sqlConnectionString, ILogger<PatientRepository> logger)
        {
            this.sqlConnectionString = sqlConnectionString;
            this.logger = logger;
        }

        public async Task<Patient> InsertAsync(Patient patient)
        {
            logger.LogInformation("Inserting patient with ID: {Id}", patient.ID);
            try
            {
                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync(@"
                        INSERT INTO [Patients] (Id, UserID, FirstName, LastName, Birthdate, NextAppointmentDate, GuardianId, TreatmentPlanId, DoctorID) 
                        VALUES (@Id, UserID, @FirstName, @LastName, @Birthdate, @NextAppointmentDate, @GuardianId, @TreatmentPlanId, @DoctorID)", patient);
                }
                logger.LogInformation("Successfully inserted patient with ID: {Id}", patient.ID);
                return patient;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error inserting patient with ID: {Id}", patient.ID);
                throw;
            }
        }

        public async Task<Patient?> ReadAsync(Guid id)
        {
            logger.LogInformation("Reading patient with ID: {Id}", id);
            try
            {
                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    var patient = await sqlConnection.QuerySingleOrDefaultAsync<Patient>(
                        "SELECT * FROM [Patients] WHERE Id = @Id", new { id });

                    if (patient == null)
                        logger.LogWarning("Patient with ID: {Id} not found", id);
                    else
                        logger.LogInformation("Successfully retrieved patient with ID: {Id}", id);

                    return patient;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error reading patient with ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> ReadAsync()
        {
            logger.LogInformation("Reading all patients");
            try
            {
                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    var patients = await sqlConnection.QueryAsync<Patient>("SELECT * FROM [Patients]");
                    logger.LogInformation("Successfully retrieved {Count} patients", patients.Count());
                    return patients;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error reading all patients");
                throw;
            }
        }

        public async Task UpdateAsync(Patient patient)
        {
            logger.LogInformation("Updating patient with ID: {Id}", patient.ID);
            try
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
                logger.LogInformation("Successfully updated patient with ID: {Id}", patient.ID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating patient with ID: {Id}", patient.ID);
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            logger.LogInformation("Deleting patient with ID: {Id}", id);
            try
            {
                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync("DELETE FROM [Patients] WHERE Id = @Id", new { id });
                }
                logger.LogInformation("Successfully deleted patient with ID: {Id}", id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting patient with ID: {Id}", id);
                throw;
            }
        }
    }
}
