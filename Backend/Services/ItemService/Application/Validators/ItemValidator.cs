using FluentValidation;
using Shared;

namespace Application.Validators;

public class ItemListValidator : AbstractValidator<List<Item>>
{
    public ItemListValidator()
    {
    }
}

