using Dapper;
using BrabantCareWebApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using BrabantCareWebApi.Interfaces;

namespace BrabantCareWebApi.Repositories
{
    public class PatientRepository : IPatientRepository<Patient>
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
                    var existingGuardian = await sqlConnection.QuerySingleOrDefaultAsync<Guardian>(@"
                    SELECT * FROM [Patients] 
                    WHERE UserID = @UserID AND FirstName = @FirstName AND LastName = @LastName",
                        new { patient.UserID, patient.FirstName, patient.LastName });

                    if (existingGuardian != null)
                    {
                        return null; // Guardian already exists
                    }
                    await sqlConnection.ExecuteAsync(@"
                        INSERT INTO [Patients] 
                        (ID, UserID, FirstName, LastName, GuardianID, TreatmentPlanID, DoctorID, Birthdate, NextAppointmentDate, GameState, Score) 
                        VALUES 
                        (@ID, @UserID, @FirstName, @LastName, @GuardianID, @TreatmentPlanID, @DoctorID, @Birthdate, @NextAppointmentDate, @GameState, @Score)", patient);

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

        public async Task<IEnumerable<Patient>> ReadByUserAsync(string userId)
        {

            logger.LogInformation("Reading all for user patients");
            try
            {
                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    var patients = await sqlConnection.QueryAsync<Patient>("SELECT * FROM [Patients] WHERE UserId = @UserId", new { UserId = userId });
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
                        UserID = @UserID,
                        FirstName = @FirstName,
                        LastName = @LastName,
                        GuardianID = @GuardianID,
                        TreatmentPlanID = @TreatmentPlanID,
                        DoctorID = @DoctorID,
                        Birthdate = @Birthdate,
                        NextAppointmentDate = @NextAppointmentDate,
                        GameState = @GameState,
                        Score = @Score
                        WHERE ID = @ID", patient);

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
