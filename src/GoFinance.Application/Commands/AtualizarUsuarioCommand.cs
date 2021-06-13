using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class AtualizarUsuarioCommand : Command
    {
        public AtualizarUsuarioCommand(Guid usuarioId, string nome, string login, string email, bool administrador, bool ativo)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Login = login;
            Email = email;
            Administrador = administrador;
            Ativo = ativo;
        }

        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Email { get; private set; }
        public bool Administrador { get; private set; }
        public bool Ativo { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarUsuarioValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarUsuarioValidator: AbstractValidator<AtualizarUsuarioCommand>
    {
        public AtualizarUsuarioValidator()
        {
            RuleFor(c=>c.UsuarioId).NotEqual(Guid.Empty).WithMessage("Id do usuario não pode estar vazio");
            RuleFor(c => c.Nome).MaximumLength(100).WithMessage("O Nome do usuário não pode ter mais de 100 caracteres");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome não pode estar vazio");
            RuleFor(c => c.Login).MaximumLength(50).WithMessage("O campo Login não pode ter mais de 50 caracteres");
            RuleFor(c => c.Login).NotNull().NotEmpty().WithMessage("O campo Login não pode estar vazio");
            RuleFor(c => c.Email).NotNull().NotEmpty().WithMessage("O campo E-mail não pode estar vazio");
            RuleFor(c => c.Email).MaximumLength(200).WithMessage("O campo E-mail não pode ter mais de 200 caracteres");
            RuleFor(c => c.Email).EmailAddress().WithMessage("E-mail inválido");
        }
    }
}
