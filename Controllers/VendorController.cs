using Microsoft.AspNetCore.Mvc;
using sample_api.DTOs;
using sample_api.Exceptions;
using sample_api.Services;

namespace sample_api.Controllers;

// TODO: 
// IMemoryCache()
// Auth & Authz
// Bicep - Infrastructure as code to create resources
// Attempt an Azure deployment
// Azure devops

[ApiController]
[Route("vendors")]
public class VendorController : ControllerBase
{
    private readonly IVendorService _vendorService;
    
    public VendorController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    // TODO:
    // Could use CreatedAtAction for maintainability here.
    [HttpPost]
    public async Task<IActionResult> CreateVendorAsync([FromBody] VendorDto vendorDto)
    {
        try
        {
            bool isCreated = await _vendorService.CreateVendorAsync(vendorDto);
            return isCreated 
                ? Created($"vendors/{vendorDto.Name}", vendorDto) 
                : Conflict($"Vendor with name: {vendorDto.Name} - already exists");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<VendorDto>>> GetVendorsAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            IReadOnlyCollection<VendorDto> vendors = await _vendorService.GetAllVendorsAsync(page, pageSize);
            return Ok(vendors);
        }
        catch (Exception)  // Update catch clause with appropriate error/responses
        {
            return StatusCode(500);
        }
    }
    
    [HttpGet("{vendorName}")]
    public async Task<ActionResult<VendorDto>> GetVendorByNameAsync(string vendorName)
    {
        try
        {
            VendorDto vendorDto = await _vendorService.GetVendorInfoAsync(vendorName);
            return Ok(vendorDto);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPut("{vendorName}")]
    public async Task<ActionResult<VendorDto>> UpdateVendorByNameAsync(string vendorName, VendorDto vendorDto)
    {
        try
        {
            if (vendorName != vendorDto.Name)
                return BadRequest($"{vendorName} in URL does not match {vendorDto.Name} in body.");

            VendorDto responseDto = await _vendorService.UpdateVendorAsync(vendorName, vendorDto);
            return Ok(responseDto);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpDelete("{vendorName}")]
    public async Task<IActionResult> DeleteVendorByNameAsync(string vendorName)
    {
        try
        {
            await _vendorService.DeleteVendorByNameAsync(vendorName);
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return NotFound($"{vendorName} not found.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}