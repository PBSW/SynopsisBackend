using FluentValidation;
using Shared;

namespace Application.Validators;

public class ItemValidator : AbstractValidator<Item>
{
    public ItemValidator()
    {
    }
}