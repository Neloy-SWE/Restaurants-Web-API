using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class CreateRestaurantDto
    {
        //[StringLength(100, MinimumLength = 5)]
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        //[Required(ErrorMessage = "Insert a valid category")]
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; } = default!;

        //[EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string ContactEmail { get; set; } = default!;

        //[Phone(ErrorMessage ="Please provide a valid phone number")]
        //[Required(ErrorMessage = "Phone number is required")]
        //[RegularExpression(@"^01\d{9}$", ErrorMessage = "Phone number must start with 01 and be 11 digits")]
        public string ContactNumber { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
    }
}
