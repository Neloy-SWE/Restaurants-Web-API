namespace Restaurants.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public Address? Address { get; set; }
        public List<Dish> Dishes { get; set; } = [];
    }
}

/*
 * as we can see here, we have declare default! for some variables. these value cannot be null.
 * if anyone try to send request with null value then [ApiController] will handle the validation
 * and send a defult json for required parameters.
 */