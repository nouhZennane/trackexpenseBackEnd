using FluentValidation;
using TrackExences.Dtos.Expenses;

namespace TrackExences.Validators;

public class UpdateExpenseValidator:AbstractValidator<UpdateExpense>
{
    public UpdateExpenseValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .When(x => x.Amount != null);
        
        
        RuleFor(x => x.Category)
            .MinimumLength(2)
            .When(x => x.Category != null);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(3)
            .When(x => x.Description != null);
    }
}