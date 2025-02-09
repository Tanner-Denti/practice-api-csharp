using sample_api.DTOs;

namespace sample_api.Services;

public interface IVendorService
{
    public Task<bool> CreateVendorAsync(VendorDto vendorDto);
    public Task<IReadOnlyCollection<VendorDto>> GetAllVendorsAsync();
    public Task<VendorDto> GetVendorInfoAsync(string vendorName);
}