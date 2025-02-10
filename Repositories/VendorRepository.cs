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
        if (await VendorExistsAsync(vendorDto))
            return false;
        
        // Do I need a transaction for this?
        await _dbContext.Vendor.AddAsync(_vendorMapper.VendorDtoToVendor(vendorDto));
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyCollection<VendorDto>> GetAllVendorsAsync(int page, int pageSize)
    {
        return await _dbContext.Vendor
            .OrderBy(v => v.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(v => _vendorMapper.VendorToVendorDto(v))
            .ToListAsync();
    }
    
    public async Task<VendorDto?> GetVendorByNameAsync(string vendorName)
    {
        Vendor? vendor = await _dbContext.Vendor.FindAsync(vendorName);
        return vendor == null ? null : _vendorMapper.VendorToVendorDto(vendor);
    }

    public async Task<VendorDto> UpdateVendorAsync(string vendorName, VendorDto vendorDto)
    {
        Vendor? existingVendor = await _dbContext.Vendor.FirstOrDefaultAsync(v => v.Name == vendorName);
        
        if (existingVendor == null)
        {
            await _dbContext.Vendor.AddAsync(_vendorMapper.VendorDtoToVendor(vendorDto));
        }
        else
        {
            existingVendor.Name = vendorDto.Name;
            existingVendor.PhoneNumber = vendorDto.PhoneNumber;
            existingVendor.Street = vendorDto.Street;
            existingVendor.City = vendorDto.City;
            existingVendor.State = vendorDto.State;
            existingVendor.PostalCode = vendorDto.PostalCode;
        }

        // Ideally these 2 lines are locked so nothing can edit vendor in the database before saved data is read back out.
        // Transaction?
        await _dbContext.SaveChangesAsync();
        Vendor vendor = await _dbContext.Vendor.FirstAsync(v => v.Name == vendorName);
        return _vendorMapper.VendorToVendorDto(vendor);
    }

    public async Task<int> DeleteVendorByName(string vendorName)
    {
        // vendorName is primary key, so we know there can't be more than 1 deletion.
        return await _dbContext.Vendor
            .Where(v => v.Name == vendorName)
            .ExecuteDeleteAsync();
    }

    private async Task<bool> VendorExistsAsync(VendorDto vendorDto)
    {
        return await _dbContext.Vendor.AnyAsync(v => v.Name == vendorDto.Name); 
    }
}