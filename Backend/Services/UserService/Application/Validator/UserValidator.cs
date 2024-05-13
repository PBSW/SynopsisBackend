using System.Data;
using FluentValidation;
using Shared;

namespace Application.Validator;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        CascadeMode = CascadeMode.Stop;
        
        RuleFor(u => u.FirstName)
            .NotNull().WithMessage("First Name is null")
            .NotEmpty().WithMessage("First Name is required")
            .MaximumLength(50).WithMessage("First Name cannot be longer than 50 characters");
        
        RuleFor(u => u.LastName)
            .NotNull().WithMessage("Last Name is null")
            .NotEmpty().WithMessage("Last Name is required")
            .MaximumLength(50).WithMessage("Last Name cannot be longer than 50 characters");
        
        RuleFor(u => u.Mail)
            .NotNull().WithMessage("Email is null")
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not a valid Email Address");
    }
}