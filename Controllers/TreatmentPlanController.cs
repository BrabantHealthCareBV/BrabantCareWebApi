using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/treatmentplans")]
public class TreatmentPlanController : ControllerBase
{
    private readonly TreatmentPlanRepository _treatmentPlanRepository;

    public TreatmentPlanController(TreatmentPlanRepository treatmentPlanRepository)
    {
        _treatmentPlanRepository = treatmentPlanRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddTreatmentPlan([FromBody] TreatmentPlan treatmentPlan)
    {
        var createdtreatmentPlan = await _treatmentPlanRepository.InsertAsync(treatmentPlan);
        return Ok(createdtreatmentPlan);
    }

    [HttpGet]
    public async Task<IEnumerable<TreatmentPlan>> GetTreatmentPlan()
    {
        return await _treatmentPlanRepository.ReadAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTreatmentPlaById(Guid id)
    {
        var treatmentPlan = await _treatmentPlanRepository.ReadAsync(id);
        if (treatmentPlan == null) return NotFound(new { message = "TreatmentPlan not found." });

        return Ok(treatmentPlan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGuardian(Guid id, [FromBody] TreatmentPlan treatmentPlan)
    {
        var existingGuardian = await _treatmentPlanRepository.ReadAsync(id);
        if (existingGuardian == null) return NotFound(new { message = "TreatmentPlan not found." });

        await _treatmentPlanRepository.UpdateAsync(treatmentPlan);
        return Ok(new { message = $"TreatmentPlan {treatmentPlan.Name}updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGuardian(Guid id)
    {
        await _treatmentPlanRepository.DeleteAsync(id);
        return Ok(new { message = "TreatmentPlan deleted successfully." });
    }
}
