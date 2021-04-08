using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(u => u.FirstName).NotNull();
            RuleFor(u => u.LastName).NotNull();
            RuleFor(u => u.Email).NotNull().EmailAddress();
            RuleFor(u => u.Password).NotNull();
            RuleFor(u => u.Password).MinimumLength(6).WithMessage("Parola en az 6 karakter olmalıdır");
            RuleFor(u => u.Password).MaximumLength(16).WithMessage("Parole en fazla 16 karakter olmalıdır");
        }
    }
}
