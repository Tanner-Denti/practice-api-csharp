using sample_api.DTOs;

namespace sample_api.Repositories;

public interface IVendorRepository
{
    public Task<bool> CreateVendorAsync(VendorDto vendorDto);
    public Task<IReadOnlyCollection<VendorDto>> GetAllVendorsAsync(); 
    public Task<VendorDto?> GetVendorByNameAsync(string vendorName);
}