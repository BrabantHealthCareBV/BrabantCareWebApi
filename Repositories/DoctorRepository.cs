using Dapper;
using Microsoft.Data.SqlClient;
using BrabantCareWebApi.Models;
using Microsoft.Extensions.Logging;

namespace ProjectMap.WebApi.Repositories
{
    public class DoctorRepository
    {
        private readonly string sqlConnectionString;
        private readonly ILogger<DoctorRepository> _logger;

        public DoctorRepository(string sqlConnectionString, ILogger<DoctorRepository> logger)
        {
            this.sqlConnectionString = sqlConnectionString;
            _logger = logger;
        }

        public async Task<Doctor> InsertAsync(Doctor doctor)
        {
            try
            {
                _logger.LogInformation("Attempting to insert doctor: {DoctorName}", doctor.Name);

                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync(
                        "INSERT INTO [Doctors] (Id, Name, Specialization) VALUES (@Id, @Name, @Specialization)", doctor);
                    _logger.LogInformation("Doctor inserted successfully: {DoctorName}", doctor.Name);
                }

                return doctor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting doctor: {DoctorName}", doctor.Name);
                throw;
            }
        }

        public async Task<Doctor?> ReadAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Fetching doctor with ID: {DoctorId}", id);

                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    var doctor = await sqlConnection.QuerySingleOrDefaultAsync<Doctor>(
                        "SELECT * FROM [Doctors] WHERE Id = @Id", new { id });

                    if (doctor != null)
                    {
                        _logger.LogInformation("Doctor found: {DoctorName}", doctor.Name);
                    }
                    else
                    {
                        _logger.LogWarning("Doctor not found with ID: {DoctorId}", id);
                    }

                    return doctor;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching doctor with ID: {DoctorId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Doctor>> ReadAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all doctors");

                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    var doctors = await sqlConnection.QueryAsync<Doctor>("SELECT * FROM [Doctors]");
                    _logger.LogInformation("Fetched {DoctorCount} doctors", doctors.Count());
                    return doctors;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all doctors");
                throw;
            }
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            try
            {
                _logger.LogInformation("Updating doctor with ID: {DoctorId}", doctor.ID);

                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync(
                        "UPDATE [Doctors] SET Name = @Name, Specialization = @Specialization WHERE Id = @Id", doctor);
                    _logger.LogInformation("Doctor updated successfully: {DoctorName}", doctor.Name);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating doctor: {DoctorName}", doctor.Name);
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting doctor with ID: {DoctorId}", id);

                using (var sqlConnection = new SqlConnection(sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync("DELETE FROM [Doctors] WHERE Id = @Id", new { id });
                    _logger.LogInformation("Doctor deleted with ID: {DoctorId}", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting doctor with ID: {DoctorId}", id);
                throw;
            }
        }
    }
}
