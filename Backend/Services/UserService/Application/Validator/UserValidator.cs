using FluentValidation;
using Shared;

namespace Application.Validator;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        CascadeMode = CascadeMode.Stop;
        
    }
}