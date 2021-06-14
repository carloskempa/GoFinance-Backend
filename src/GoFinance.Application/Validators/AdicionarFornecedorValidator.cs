using FluentValidation;
using GoFinance.Application.Commands;
using System;

namespace GoFinance.Application.Validators
{
    public class AdicionarFornecedorValidator : AbstractValidator<AdicionarFornecedorCommand>
    {
        public AdicionarFornecedorValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("O campo nome do fornecedor não pode estar vazio")
                                .MaximumLength(100).WithMessage("O campo nome do Fornecedor pode ter no máximo 100 caracteres");

            RuleFor(c => c.CnpjCpf).NotEmpty().WithMessage("O campo CNPJ/CPF do fornecedor não pode estar vazio")
                                   .MaximumLength(14).WithMessage("O campo CNPJ/CPF do Fornecedor pode ter no máximo 14 caracteres");

            RuleFor(c => c.UrlSite).MaximumLength(250).WithMessage("O campo Url pode ter no máximo 250 caracteres");
            
            RuleFor(c => c.Descricao).MaximumLength(500).WithMessage("O campo Descrição do Fornecedor pode ter no máximo 500 caracteres");
            
            RuleFor(c => c.UsuarioId).NotEqual(Guid.Empty).WithMessage("Id do usuario inválido");
        }
    }

}
