using CompliantManager.Server.Extensions;
using CompliantManager.Server.Services.Interfaces;
using CompliantManager.Shared.Dtos;
using CompliantManager.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompliantManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClaimController(IClaimService claimService, IProductService productService) : ControllerBase
    {
        private readonly IClaimService _claimService = claimService;
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetClaims([FromQuery] int count, [FromQuery] int offset, [FromQuery] ListMode mode, [FromQuery] Guid? userId = null)
        {
            var claims = await _claimService.GetAll(offset, count, mode, userId);

            if (claims.Count == 0)
                return NotFound("No users found.");

            var claimDtos = claims.Select(u => u.ToDto()).ToList();

            return Ok(claimDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(ClaimDto claimDto)
        {
            var claim = claimDto.ToEntity();
            await _claimService.Create(claim);

            return Ok(claim.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaim(int id)
        {
            var claim = await _claimService.GetById(id);
            if (claim is null)
                return NotFound("Claim not found.");
            
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
            var productsToDelete = claimDto.Order?.Products
            .Where(p => p.IsDeleted)
            .Select(p => p.Id)
            .ToList() ?? [];

            if (productsToDelete.Count > 0)
            {
                foreach (var productId in productsToDelete)
                {
                    await _productService.DeleteAsync(productId);
                }
            }

            claimDto.Order.Products = claimDto.Order.Products.Where(p => !p.IsFromDatabase && !p.IsDeleted).ToList();

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
