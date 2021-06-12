using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoFinance.Application.Commands
{
    public class AuthenticarUsuarioCommand: Command
    {
        public AuthenticarUsuarioCommand(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        public string Login { get; private set; }
        public string Senha { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AuthenticarUsuarioValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
    public class AuthenticarUsuarioValidator: AbstractValidator<AuthenticarUsuarioCommand>
    {
        public AuthenticarUsuarioValidator()
        {
            RuleFor(c => c.Login).MaximumLength(50).WithMessage("Login inválido.");
            RuleFor(c => c.Login).NotEmpty().NotNull().WithMessage("Login não pode estar vazio.");
            RuleFor(c => c.Senha).NotEmpty().NotNull().WithMessage("Senha não pode estar vazio.");
        }
    }

}
