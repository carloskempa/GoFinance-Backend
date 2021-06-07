using FluentValidation;
using GoFinance.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoFinance.Application.Validators
{
    public class AdicionarUsuarioValidator : AbstractValidator<AdicionarUsuarioCommand>
    {
        public AdicionarUsuarioValidator()
        {

        }
    }
}
