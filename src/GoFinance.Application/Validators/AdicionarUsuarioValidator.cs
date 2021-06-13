using FluentValidation;
using GoFinance.Application.Commands;

namespace GoFinance.Application.Validators
{
    public class AdicionarUsuarioValidator : AbstractValidator<AdicionarUsuarioCommand>
    {
        public AdicionarUsuarioValidator()
        {
            RuleFor(c => c.Nome).MaximumLength(100).WithMessage("O Nome do usuário não pode ter mais de 100 caracteres");
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage("O campo Nome não pode estar vazio");
            RuleFor(c => c.Login).MaximumLength(50).WithMessage("O campo Login não pode ter mais de 50 caracteres");
            RuleFor(c => c.Login).NotNull().NotEmpty().WithMessage("O campo Login não pode estar vazio");
            RuleFor(c => c.Senha).MaximumLength(20).WithMessage("O campo Senha não pode ter mais de 20 caracteres");
            RuleFor(c => c.Senha).MinimumLength(6).WithMessage("O campo Senha tem que ter no minímo 6 caracteres");
            RuleFor(c => c.Senha).NotNull().NotEmpty().WithMessage("O campo Senha não pode estar vazio");
            RuleFor(c => c.Email).NotNull().NotEmpty().WithMessage("O campo E-mail não pode estar vazio");
            RuleFor(c => c.Email).MaximumLength(200).WithMessage("O campo E-mail não pode ter mais de 200 caracteres");
            RuleFor(c => c.Email).EmailAddress().WithMessage("E-mail inválido");
        }
    }
}
