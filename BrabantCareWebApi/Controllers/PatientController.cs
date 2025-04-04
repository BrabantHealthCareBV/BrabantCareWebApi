using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
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
    [HttpPost]
    public async Task<IActionResult> AddPatient([FromBody] Patient patient)
    {
        try
        {
            Console.WriteLine($"add patient with id: {patient.ID}");
            if (patient == null)
            {
                return BadRequest(new { message = "Invalid patient data." });
            }

            var guardianExists = await _guardianRepository.DoesGuardianExistAsync(patient.guardianID);
            if (!guardianExists)
            {
                return BadRequest(new { message = "Guardian does not exist. Please provide a valid GuardianID." });
            }

            var treatmentPlanExists = await _treatmentPlanRepository.DoesTreatmentPlanExistAsync(patient.treatmentPlanID);
            if (!treatmentPlanExists)
            {
                return BadRequest(new { message = "Treatment plan does not exist. Please provide a valid TreatmentPlanID." });
            }

            if (patient.ID == Guid.Empty || patient.ID == null)
            {
                patient.ID = Guid.NewGuid();
            }



            if (patient.doctorID != Guid.Empty && patient.doctorID != null)
            {
                var doctorExists = await _doctorRepository.DoesDoctorExistAsync(patient.doctorID.Value);
                if (!doctorExists)
                {
                    return BadRequest(new { message = "Doctor does not exist. Please provide a valid DoctorID or leave it empty." });
                }
            }
            else
            {
                patient.doctorID = _doctorRepository.ReadAsync().Result.First().ID;
            }

            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            patient.UserID = userId;

            var createdPatient = await _patientRepository.InsertAsync(patient);
            if (createdPatient == null)

                return NoContent();
            else
                return Ok(createdPatient);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding the patient.", error = ex.Message });
        }
    }


    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        try
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            var patients = await _patientRepository.ReadByUserAsync(userId);
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

            var guardianExists = await _guardianRepository.DoesGuardianExistAsync(patient.guardianID);
            if (!guardianExists)
            {
                return BadRequest(new { message = "Guardian does not exist. Please provide a valid GuardianID." });
            }

            var treatmentPlanExists = await _treatmentPlanRepository.DoesTreatmentPlanExistAsync(patient.treatmentPlanID);
            if (!treatmentPlanExists)
            {
                return BadRequest(new { message = "Treatment plan does not exist. Please provide a valid TreatmentPlanID." });
            }

            if (patient.ID == Guid.Empty)
            {
                patient.ID = Guid.NewGuid();
            }


            var existingPatient = await _patientRepository.ReadAsync(id);
            if (existingPatient == null)
            {
                return NotFound(new { message = "Patient not found." });
            }
            if (patient.ID == Guid.Empty)
            {
                patient.ID = existingPatient.ID;
            }
            if (patient.UserID.IsNullOrEmpty())
            {
                patient.UserID = existingPatient.UserID;
            }
            if (patient.doctorID == Guid.Empty)
            {
                patient.doctorID = null;
            }

            await _patientRepository.UpdateAsync(patient);
            return Ok(patient);
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
