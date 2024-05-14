using FluentValidation;
using Shared;

namespace Application.Validators;

public class ToDoValidator : AbstractValidator<ToDoList>
{
    public ToDoValidator()
    {
        CascadeMode = CascadeMode.Stop;
    }
}