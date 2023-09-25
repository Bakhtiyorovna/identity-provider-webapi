using FluentValidation;
using Idenitity_Provider.Persistence.Dtos.Auth;

namespace Idenitity_Provider.Persistence.Validators.Auth;

public class UserRegisterValidator : AbstractValidator<RegisterDto>

{
    public UserRegisterValidator()
    {
        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required!")
            .MinimumLength(3).WithMessage("FirstName must be less than 3 characters")
                .MaximumLength(30).WithMessage("FirstName must be less than 30 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("LastName is required!")
            .MinimumLength(3).WithMessage("LastName must be less than 3 characters")
                .MaximumLength(30).WithMessage("LastName must be less than 30 characters");

        RuleFor(dto => dto.Password).Must(passsword => PasswordValidator.IsStrongPassword(passsword).IsValid);
    }
}
