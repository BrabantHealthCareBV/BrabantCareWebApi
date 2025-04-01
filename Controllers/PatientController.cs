using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly PatientRepository _patientRepository;

    public PatientController(PatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddPatient([FromBody] Patient patient)
    {
        try
        {
            if (patient == null)
            {
                return BadRequest(new { message = "Invalid patient data." });
            }

            var createdPatient = await _patientRepository.InsertAsync(patient);
            return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.ID }, createdPatient);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding the patient.", error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        try
        {
            var patients = await _patientRepository.ReadAsync();
            return Ok(patients);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving patients.", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientById(Guid id)
    {
        try
        {
            var patient = await _patientRepository.ReadAsync(id);
            if (patient == null)
                return NotFound(new { message = "Patient not found." });

            return Ok(patient);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the patient.", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] Patient patient)
    {
        try
        {
            if (patient == null)
            {
                return BadRequest(new { message = "Invalid patient data." });
            }

            var existingPatient = await _patientRepository.ReadAsync(id);
            if (existingPatient == null)
            {
                return NotFound(new { message = "Patient not found." });
            }

            await _patientRepository.UpdateAsync(patient);
            return Ok(new { message = $"Patient {patient.FirstName} {patient.LastName} updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the patient.", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        try
        {
            var existingPatient = await _patientRepository.ReadAsync(id);
            if (existingPatient == null)
            {
                return NotFound(new { message = "Patient not found." });
            }

            await _patientRepository.DeleteAsync(id);
            return Ok(new { message = "Patient deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the patient.", error = ex.Message });
        }
    }
}
