using ExampleAPI.Models;
using FluentValidation;

namespace ExampleAPI.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(2, 100).WithMessage("Title length should be between 2 to 100 characters.");

        RuleFor(x => x.YearOfRelease)
            .NotEmpty().WithMessage("Year is required.")
            .InclusiveBetween(1900, DateTime.Now.Year).WithMessage("Year should be between 1900 and current year.");
    }
}