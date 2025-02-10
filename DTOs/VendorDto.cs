using System.ComponentModel.DataAnnotations;

namespace sample_api.DTOs;

public class VendorDto
{
    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }
    
    [MaxLength(50)]
    [Phone]
    public string? PhoneNumber { get; set; }
    
    [MaxLength(50)]
    public string? Street { get; set; }
    
    [MaxLength(50)]
    public string? City { get; set; }
    
    [MaxLength(50)]
    public string? State { get; set; }
    
    [Required]
    [MaxLength(10)]
    public string? PostalCode { get; set; }
}