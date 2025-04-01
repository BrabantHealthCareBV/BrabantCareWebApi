using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly PatientRepository _patientRepository;
    private readonly GuardianRepository _guardianRepository;
    private readonly TreatmentPlanRepository _treatmentPlanRepository;
    private readonly DoctorRepository _doctorRepository;
    private readonly IAuthenticationService _authenticationService;

    public PatientController(PatientRepository patientRepository, GuardianRepository guardianRepository, TreatmentPlanRepository treatmentPlanRepository, DoctorRepository doctorRepository, IAuthenticationService authenticationService)
    {
        _patientRepository = patientRepository;
        _guardianRepository = guardianRepository;
        _doctorRepository = doctorRepository;
        _treatmentPlanRepository = treatmentPlanRepository;
        _authenticationService = authenticationService;
    }
    public async Task<bool> DoesGuardianExistAsync(Guid guardianId)
    {
        var guardians = await _guardianRepository.ReadAsync();
        return guardians.Any(g => g.ID == guardianId);
    }

    public async Task<bool> DoesDoctorExistAsync(Guid doctorId)
    {
        var doctors = await _doctorRepository.ReadAsync();
        return doctors.Any(d => d.ID == doctorId);
    }

    public async Task<bool> DoesTreatmentPlanExistAsync(Guid treatmentPlanId)
    {
        var treatmentPlans = await _treatmentPlanRepository.ReadAsync();
        return treatmentPlans.Any(tp => tp.ID == treatmentPlanId);
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

            var guardianExists = await DoesGuardianExistAsync(patient.GuardianID);
            if (!guardianExists)
            {
                return BadRequest(new { message = "Guardian does not exist. Please provide a valid GuardianID." });
            }

            var treatmentPlanExists = await DoesTreatmentPlanExistAsync(patient.TreatmentPlanID);
            if (!treatmentPlanExists)
            {
                return BadRequest(new { message = "Treatment plan does not exist. Please provide a valid TreatmentPlanID." });
            }

            if (patient.ID == Guid.Empty)
            {
                patient.ID = Guid.NewGuid();
            }



            if (patient.DoctorID.HasValue)
            {
                var doctorExists = await DoesDoctorExistAsync(patient.DoctorID.Value);
                if (!doctorExists)
                {
                    return BadRequest(new { message = "Doctor does not exist. Please provide a valid DoctorID or leave it empty." });
                }
            }
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            patient.UserID = userId;

            var createdPatient = await _patientRepository.InsertAsync(patient);

            // Return the created patient with the generated ID
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
