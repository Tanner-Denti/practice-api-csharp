using Microsoft.AspNetCore.Mvc;
using sample_api.DTOs;
using sample_api.Exceptions;
using sample_api.Services;

namespace sample_api.Controllers;

// TODO:
// Pagination
// Auth & Authz
// Telemetry

[ApiController]
[Route("[controller]")]
public class VendorController : ControllerBase
{
    private readonly IVendorService _vendorService;
    
    public VendorController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVendorAsync([FromBody] VendorDto vendorDto)
    {
        try
        {
            bool isCreated = await _vendorService.CreateVendorAsync(vendorDto);
            return isCreated 
                ? Created($"Vendor/{vendorDto.Name}", vendorDto)
                : Conflict($"Vendor with name: {vendorDto.Name} - already exists");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<VendorDto>>> GetVendorsAsync()
    {
        try
        {
            IReadOnlyCollection<VendorDto> vendors = await _vendorService.GetAllVendorsAsync();
            return Ok(vendors);
        }
        catch (Exception)  // Update catch clause with appropriate error/responses
        {
            return StatusCode(500);
        }
    }
    
    [HttpGet("{vendorName}", Name = "GetVendorByName")]
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
}