using System.ComponentModel.DataAnnotations;


namespace Business.Models;

public class AddressInfoModel
{
    public int Id { get; set; }

    [Display(Name = "Address line 1", Prompt = "Enter your address line", Order = 0)]
    [Required(ErrorMessage = "Address line 1 is required")]
    [MinLength(2, ErrorMessage = "Name too short")]
    public string AddresLine_1 { get; set; } = null!;

    [Display(Name = "Address line 2", Prompt = "Enter your second address line", Order = 0)]
    public string? AddressLine_2 { get; set; }

    [Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 0)]
    [DataType(DataType.PostalCode)]
    [Required(ErrorMessage = "Postal code is required")]
    [RegularExpression("^\\d{3}\\s\\d{2}$", ErrorMessage = "Invalid Postal code")]
    [MinLength(6, ErrorMessage = "Postal Code too short")]
    [MaxLength(6, ErrorMessage = "Postal Code too long")]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City", Prompt = "Enter your city", Order = 0)]
    [Required(ErrorMessage = "City is required")]
    [MinLength(2, ErrorMessage = "Name too short")]
    public string City { get; set; } = null!;
}
