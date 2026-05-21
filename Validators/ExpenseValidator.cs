using FluentValidation;
using TrackExences.Dtos.Expenses;

namespace TrackExences.Validators;

public class ExpenseValidator: AbstractValidator<CreateExpense>
{
    public ExpenseValidator()
    {
        RuleFor(e => e.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0")
            .NotEmpty().WithMessage("Amount must not be empty")
            .NotNull().WithMessage("Amount must not be null");

        RuleFor(e => e.Category)
            .NotEmpty().WithMessage("Category must not be empty")
            .NotNull().WithMessage("Category must not be null")
            ;

    }
}