using sample_api.Data.Entities;
using sample_api.DTOs;

namespace sample_api.Mappers;

public interface IVendorMapper
{
    public VendorDto VendorToVendorDto(Vendor vendor);
    public Vendor VendorDtoToVendor(VendorDto vendorDto);
}