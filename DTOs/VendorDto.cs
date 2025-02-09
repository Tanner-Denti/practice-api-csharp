using System.ComponentModel.DataAnnotations;

namespace sample_api.DTOs;

public class VendorDto
{
    [Required]
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    [Required]
    public string? PostalCode { get; set; }
}