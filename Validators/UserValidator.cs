using FluentValidation;
using TrackExences.Dtos.User;

namespace TrackExences.Validators;

public class UserValidator: AbstractValidator<CreateUser>
{
   public UserValidator()
   {
       RuleFor(x => x.Name)
           .Length(1, 100).WithMessage("Name must be between 1 and 100 characters long")
           .NotEmpty().WithMessage("Name cannot be empty")
           .NotNull().WithMessage("Name cannot be null");
       
       RuleFor(x => x.nickname)
           .Length(1, 100).WithMessage("NickName must be between 1 and 100 characters long")
           .NotEmpty().WithMessage("NickName cannot be empty")
           .NotNull().WithMessage("NickName cannot be null");
           
       RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty")
           .NotEmpty().WithMessage("Email cannot be empty")
           .NotNull().WithMessage("Email cannot be null")
           .EmailAddress().WithMessage("Email address cannot be empty")
           ;
       RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password cannot be empty")
           .MinimumLength(2).WithMessage("Password must be at least 2 characters long")
           .Matches("[A-Z]").WithMessage("Must contain uppercase")
           .Matches("[a-z]").WithMessage("Must contain lowercase")
           .Matches("[0-9]").WithMessage("Must contain number")
           ;

   }
}