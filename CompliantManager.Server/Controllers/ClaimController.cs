using CompliantManager.Server.Extensions;
using CompliantManager.Server.Services.Interfaces;
using CompliantManager.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompliantManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClaimController(IClaimService claimService) : ControllerBase
    {
        private readonly IClaimService _claimService = claimService;

        [HttpGet]
        public async Task<IActionResult> GetClaims([FromQuery] int count, [FromQuery] int offset)
        {
            var claims = await _claimService.GetAll(offset, count);

            if (claims.Count == 0)
                return NotFound("No users found.");

            var claimDtos = claims.Select(u => u.ToDto()).ToList();

            return Ok(claimDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var claim = await _claimService.GetById(id);
            if (claim is null)
                return NotFound("User not found.");
            
            var claimDto = claim.ToDto();
            return Ok(claimDto);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetClaimsCount()
        {
            return Ok(await _claimService.GetCount());
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateClaim([FromBody] ClaimDto claimDto)
        {
            await _claimService.Edit(claimDto.ToEntity());

            return Ok("Claim updated successfully.");
        }

        [HttpPost("deleteMultiple")]
        public async Task<IActionResult> DeleteClaims([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return BadRequest("No claim IDs provided.");
            
            foreach (var id in ids)
            {
                await _claimService.Delete(id);
            }

            return Ok("Users deleted successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            var result = await _claimService.Delete(id);
            return result ? Ok("User deleted successfully.") : BadRequest();
        }
    }
}
