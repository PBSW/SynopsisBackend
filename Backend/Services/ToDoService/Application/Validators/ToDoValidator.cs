using FluentValidation;
using Shared;

namespace Application.Validators;

public class 
    ToDoValidator : AbstractValidator<ToDoList>
{
    public ToDoValidator()
    {
        CascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId is null")
            .NotEmpty().WithMessage("UserId is required")
            .GreaterThan(0).WithMessage("UserId must be greater than 0");
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(50)
            .WithMessage("Title must not exceed 50 characters");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Items is null");
    }
}