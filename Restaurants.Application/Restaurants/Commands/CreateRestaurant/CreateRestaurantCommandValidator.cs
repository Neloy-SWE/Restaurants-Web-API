using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "Chinese", "Indian", "French", "Japanese"];
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(5, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(dto => dto.Category)
                .NotEmpty().WithMessage("Insert a valid category")
                .Must(validCategories.Contains)
                .WithMessage("Category must be one of the following: " + string.Join(", ", validCategories));
            //.Custom((value, context) =>
            //{
            //    var isValidCategory = validCategories.Contains(value);
            //    if (!isValidCategory)
            //    {
            //        context.AddFailure("Category must be one of the following: " + string.Join(", ", validCategories));
            //    }
            //});

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.ContactNumber)
                .Length(11)
                .Matches(@"^01\d{9}$")
                .WithMessage("Phone number must start with 01 and be 11 digits");
        }
    }
}
