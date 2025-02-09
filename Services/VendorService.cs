using sample_api.DTOs;
using sample_api.Exceptions;
using sample_api.Repositories;

namespace sample_api.Services;

// Finish writing GetVendorInfoAync
// Finish writing CreateVendorAsync
public class VendorService : IVendorService
{
    private readonly IVendorRepository _vendorRepository;

    public VendorService(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public async Task<bool> CreateVendorAsync(VendorDto vendorDto)
    {
        return await _vendorRepository.CreateVendorAsync(vendorDto);
    }

    public async Task<IReadOnlyCollection<VendorDto>> GetAllVendorsAsync()
    {
        return await _vendorRepository.GetAllVendorsAsync();
    }
    
    public async Task<VendorDto> GetVendorInfoAsync(string vendorName)
    {
        VendorDto? vendorDto = await _vendorRepository.GetVendorByNameAsync(vendorName);
        if (vendorDto == null)
            throw new EntityNotFoundException("vendor", vendorName);

        return vendorDto;
    }
}