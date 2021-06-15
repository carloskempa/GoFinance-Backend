using FluentValidation;
using GoFinance.Application.Commands;

namespace GoFinance.Application.Validators
{
    public class AdicionarSmtpValidator : AbstractValidator<AdicionarSmtpCommand>
    {
        public AdicionarSmtpValidator()
        {
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
