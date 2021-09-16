﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models.Country;

namespace UserService.Validators
{
    public class CountryCreatingDtoValidator: AbstractValidator<CountryCreatingDto>
    {
        public CountryCreatingDtoValidator()
        {
            RuleFor(x => x.CountryName).NotNull().NotEmpty().MaximumLength(50).MinimumLength(3);

        }
    }
}
