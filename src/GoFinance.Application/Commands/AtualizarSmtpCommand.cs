using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class AtualizarSmtpCommand : Command
    {
        public AtualizarSmtpCommand(Guid id, string email, string mascara, string host, int porta, string usuario, string senha, bool sSL, bool ativo)
        {
            SmtpId = id;
            Email = email;
            Mascara = mascara;
            Host = host;
            Porta = porta;
            Usuario = usuario;
            Senha = senha;
            SSL = sSL;
            Ativo = ativo;
        }
        public Guid SmtpId { get;private set; }
        public string Email { get; private set; }
        public string Mascara { get; private set; }
        public string Host { get; private set; }
        public int Porta { get; private set; }
        public string Usuario { get; private set; }
        public string Senha { get; private set; }
        public bool SSL { get; private set; }
        public bool Ativo { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarSmtpValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class AtualizarSmtpValidator : AbstractValidator<AtualizarSmtpCommand>
    {
        public AtualizarSmtpValidator()
        {

            RuleFor(c => c.SmtpId).NotEqual(Guid.Empty).WithMessage("Id Smtp é inválido");

            RuleFor(c => c.Email).NotEmpty().WithMessage("O campo e-mail não pode ser vazio")
                                 .Length(1, 200).WithMessage("O campo e-mail pode ter no máximo 200 caracteres")
                                 .EmailAddress().WithMessage("E-mail informado é inválido");

            RuleFor(c => c.Mascara).NotEmpty().WithMessage("O campo máscara não pode ser vazio")
                                   .Length(2, 100).WithMessage("O campo máscara pode ter no máximo 100 caracters");

            RuleFor(c => c.Host).NotEmpty().WithMessage("O campo host não pode ser vazio")
                                .Length(2, 150).WithMessage("O campo host pode ter no máximo 150 caracters");

            RuleFor(c => c.Usuario).NotEmpty().WithMessage("O campo usuário não pode ser vazio")
                                   .Length(2, 150).WithMessage("O campo usuário pode ter no máximo 150 caracters");

            RuleFor(c => c.Senha).NotEmpty().WithMessage("O campo senha não pode ser vazio")
                                 .Length(2, 100).WithMessage("O campo senha pode ter no máximo 100 caracters");

            RuleFor(c => c.Porta).GreaterThan(0).WithMessage("O Campo porta não pode ser menor ou igual a zero");
        }
    }

}
