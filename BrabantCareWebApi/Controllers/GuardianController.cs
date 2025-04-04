using BrabantCareWebApi.Models;
using BrabantCareWebApi.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/guardians")]
public class GuardianController : ControllerBase
{
    private readonly GuardianRepository _guardianRepository;
    private readonly IAuthenticationService _authenticationService;

    public GuardianController(GuardianRepository guardianRepository, IAuthenticationService authenticationService)
    {
        _guardianRepository = guardianRepository;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> AddGuardian([FromBody] Guardian guardian)
    {

        var userId = _authenticationService.GetCurrentAuthenticatedUserId();
        guardian.UserID = userId;
        var createdGuardian = await _guardianRepository.InsertAsync(guardian);
        if (createdGuardian == null)

            return BadRequest(new { message = "Guardian Already exists for UserID." });
        else
            return Ok(createdGuardian);
    }

    [HttpGet]
    public async Task<IEnumerable<Guardian>> GetGuardians()
    {
        string? userId = _authenticationService.GetCurrentAuthenticatedUserId();
        return await _guardianRepository.ReadByUserAsync(userId);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGuardianById(Guid id)
    {
        try
        {
            var guardian = await _guardianRepository.ReadAsync(id);
            if (guardian == null)
                return NotFound(new { message = "Guardian not found." });

            return Ok(guardian);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the guardian.", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGuardian(Guid id, [FromBody] Guardian guardian)
    {
        var existingGuardian = await _guardianRepository.ReadAsync(id);
        if (existingGuardian == null) return NotFound(new { message = "Guardian not found." });
        
        await _guardianRepository.UpdateAsync(guardian); 
        return Ok(guardian);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGuardian(Guid id)
    {
        await _guardianRepository.DeleteAsync(id);
        return Ok(new { message = "Guardian deleted successfully." });
    }
}
