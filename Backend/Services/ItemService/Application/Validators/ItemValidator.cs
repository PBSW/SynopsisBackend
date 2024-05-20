using FluentValidation;
using Shared;

namespace Application.Validators;

public class ItemListValidator : AbstractValidator<List<Item>>
{
    public ItemListValidator()
    {
        CascadeMode = CascadeMode.Stop;
        
        RuleFor(i => i).NotNull().WithMessage("Item list cannot be null");
        RuleForEach(i => i).NotNull().WithMessage("Item in list cannot be null");
        RuleForEach(i => i).SetValidator(new ItemValidator());
    }
}

public class ItemValidator : AbstractValidator<Item>
{
    public ItemValidator()
    {
        CascadeMode = CascadeMode.Stop;
        
        RuleFor(i => i).NotNull().WithMessage("Item cannot be null");
        RuleFor(i => i.DateCreated).NotNull().WithMessage("Item date created cannot be null");
        RuleFor(i => i.ToDoListId).GreaterThan(0).WithMessage("Item ToDoListId must be greater than 0");
    }
}
