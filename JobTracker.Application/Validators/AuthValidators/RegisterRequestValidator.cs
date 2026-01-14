using FluentValidation;
using FluentValidation.Validators;
using JobTracker.Application.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Validators.AuthValidators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {

        public RegisterRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Password must contain uppercase")
                .Matches("[a-z]").WithMessage("Password must contain lowercase")
                .Matches("[0-9]").WithMessage("Password must contain number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain special character");



        }


    }
}
