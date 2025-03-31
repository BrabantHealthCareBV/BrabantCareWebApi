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
        var createdPatient = await _patientRepository.InsertAsync(patient);
        return Ok(createdPatient);
    }

    [HttpGet]
    public async Task<IEnumerable<Patient>> GetAllPatients()
    {
        return await _patientRepository.ReadAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientById(Guid id)
    {
        var patient = await _patientRepository.ReadAsync(id);
        if (patient == null) return NotFound(new { message = "Patient not found." });

        return Ok(patient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] Patient patient)
    {
        var existingPatient = await _patientRepository.ReadAsync(id);
        if (existingPatient == null) return NotFound(new { message = "Patient not found." });

        await _patientRepository.UpdateAsync(patient);
        return Ok(new { message = $"Patient {patient.FirstName} {patient.LastName} updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        await _patientRepository.DeleteAsync(id);
        return Ok(new { message = "Patient deleted successfully." });
    }
}
