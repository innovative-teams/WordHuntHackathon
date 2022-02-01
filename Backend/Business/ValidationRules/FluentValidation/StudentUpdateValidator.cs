using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class StudentUpdateValidator : FluentValidator<StudentForUpdateDto>
    {
        public StudentUpdateValidator()
        {

            RuleFor(s => s.NewPassword).MinimumLength(8)
                .WithMessage(Translates["Password_Must_Be_At_Least_8_Characters_Long"]);

            RuleFor(s => s.NewPassword).Must(PasswordValidator.MustContainsLowerChar)
                .WithMessage(Translates["Password_Must_Contain_At_Least_1_Lowercase_Letter"]);

            RuleFor(s => s.NewPassword).Must(PasswordValidator.MustContainsUpperChar)
                .WithMessage(Translates["Password_Must_Contain_At_Least_1_Uppercase_Letter"]);

            RuleFor(s => s.NewPassword).Must(PasswordValidator.MustContainsSpecialChar)
                .WithMessage(Translates["Password_Must_Contain_At_Least_1_Special_Character"]);

            RuleFor(s => s.NewPassword).Must(PasswordValidator.MustContainsNumberChar)
                .WithMessage(Translates["Password_Must_Contain_At_Least_1_Digit"]);
        }
    }

}
