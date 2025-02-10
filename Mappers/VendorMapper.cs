using sample_api.Data.Entities;
using sample_api.DTOs;

namespace sample_api.Mappers;

public class VendorMapper : IVendorMapper
{
    public VendorDto VendorToVendorDto(Vendor vendor)
    {
        return new VendorDto
        {
            Name = vendor.Name,
            PhoneNumber = vendor.PhoneNumber,
            Street = vendor.Street,
            City = vendor.City,
            State = vendor.State,
            PostalCode = vendor.PostalCode
        };
    }

    public Vendor VendorDtoToVendor(VendorDto vendorDto)
    {
        return new Vendor()
        {
            Name = vendorDto.Name,
            PhoneNumber = vendorDto.PhoneNumber,
            Street = vendorDto.Street,
            City = vendorDto.City,
            State = vendorDto.State,
            PostalCode = vendorDto.PostalCode
        };
    }
}