using FluentValidation;
using JobTracker.Application.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Validators.AuthValidators
{ 
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator() {

            RuleFor(x => x.Email)
               .NotEmpty()
               .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();

        }
    }
}
