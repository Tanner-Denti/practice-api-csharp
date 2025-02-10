using sample_api.DTOs;

namespace sample_api.Services;

public interface IVendorService
{
    public Task<bool> CreateVendorAsync(VendorDto vendorDto);
    public Task<IReadOnlyCollection<VendorDto>> GetAllVendorsAsync(int page, int pageSize);
    public Task<VendorDto> GetVendorInfoAsync(string vendorName);
    public Task<VendorDto> UpdateVendorAsync(string vendorName, VendorDto vendorDto);
    public Task DeleteVendorByNameAsync(string vendorName);
}