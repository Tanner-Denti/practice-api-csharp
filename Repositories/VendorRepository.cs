using Microsoft.EntityFrameworkCore;
using sample_api.Data;
using sample_api.Data.Entities;
using sample_api.DTOs;
using sample_api.Mappers;


namespace sample_api.Repositories;

public class VendorRepository : IVendorRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IVendorMapper _vendorMapper;

    public VendorRepository(ApplicationDbContext dbContext, IVendorMapper vendorMapper)
    {
        _dbContext = dbContext;
        _vendorMapper = vendorMapper;
    }

    public async Task<bool> CreateVendorAsync(VendorDto vendorDto)
    {
        bool exists = await _dbContext.Vendor.AnyAsync(v => v.Name == vendorDto.Name); 
        if (exists)
            return false;
        
        // Do I need a transaction for this?
        await _dbContext.Vendor.AddAsync(_vendorMapper.VendorDtoToVendor(vendorDto));
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyCollection<VendorDto>> GetAllVendorsAsync()
    {
        return await _dbContext.Vendor
            .Select(v => _vendorMapper.VendorToVendorDto(v))
            .ToListAsync();
    }
    
    public async Task<VendorDto?> GetVendorByNameAsync(string vendorName)
    {
        Vendor? vendor = await _dbContext.Vendor.FindAsync(vendorName);
        return vendor == null ? null : _vendorMapper.VendorToVendorDto(vendor);
    }
}