using Microsoft.Extensions.Caching.Memory;
using sample_api.DTOs;
using sample_api.Exceptions;
using sample_api.Repositories;


namespace sample_api.Services;

public class VendorService : IVendorService
{
    private readonly IVendorRepository _vendorRepository;
    private readonly ILogger<VendorService> _logger;
    private readonly IMemoryCache _cache;

    public VendorService(IVendorRepository vendorRepository, ILogger<VendorService> logger, IMemoryCache cache)
    {
        _vendorRepository = vendorRepository;
        _logger = logger;
        _cache = cache;
    }

    public async Task<bool> CreateVendorAsync(VendorDto vendorDto)
    {
        return await _vendorRepository.CreateVendorAsync(vendorDto);
    }

    public async Task<IReadOnlyCollection<VendorDto>> GetAllVendorsAsync(int page, int pageSize)
    {
        _logger.LogInformation($"Retrieving {pageSize} vendors, starting from page {page}.");
        return await _vendorRepository.GetAllVendorsAsync(page, pageSize);
    }
    
    public async Task<VendorDto> GetVendorInfoAsync(string vendorName)
    {
        // Notice out keyword
        if (!_cache.TryGetValue(vendorName, out VendorDto? vendorDto))
        {
            vendorDto = await _vendorRepository.GetVendorByNameAsync(vendorName);

            _cache.Set(vendorName, vendorDto, TimeSpan.FromSeconds(15));
        }
        
        if (vendorDto == null)
            throw new EntityNotFoundException("vendor", vendorName);

        return vendorDto;
    }

    public async Task<VendorDto> UpdateVendorAsync(string vendorName, VendorDto vendorDto)
    {
        return await _vendorRepository.UpdateVendorAsync(vendorName, vendorDto);
    }

    public async Task DeleteVendorByNameAsync(string vendorName)
    {
        int deletedRowCount = await _vendorRepository.DeleteVendorByName(vendorName);
        if (deletedRowCount == 0)
            throw new EntityNotFoundException($"{vendorName} not found.");
    }
}