using sample_api.DTOs;

namespace sample_api.Repositories;

public interface IVendorRepository
{
    public Task<bool> CreateVendorAsync(VendorDto vendorDto);
    public Task<IReadOnlyCollection<VendorDto>> GetAllVendorsAsync(int page, int pageSize); 
    public Task<VendorDto?> GetVendorByNameAsync(string vendorName);
    public Task<VendorDto> UpdateVendorAsync(string vendorName, VendorDto vendorDto);
    public Task<int> DeleteVendorByName(string vendorName);
}