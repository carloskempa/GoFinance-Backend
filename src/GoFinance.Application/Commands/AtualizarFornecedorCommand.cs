using FluentValidation;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class AtualizarFornecedorCommand : Command
    {
        public AtualizarFornecedorCommand(Guid fornecedorId, string nome, string cnpjCpf, string urlSite, string descricao, Guid usuarioId)
        {
            FornecedorId = fornecedorId;
            Nome = nome;
            CnpjCpf = cnpjCpf;
            UrlSite = urlSite;
            Descricao = descricao;
            UsuarioId = usuarioId;
        }

        public Guid FornecedorId { get; private set; }
        public string Nome { get; private set; }
        public string CnpjCpf { get; private set; }
        public string UrlSite { get; private set; }
        public string Descricao { get; private set; }
        public Guid UsuarioId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarFornecedorValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class AtualizarFornecedorValidator : AbstractValidator<AtualizarFornecedorCommand>
    {
        public AtualizarFornecedorValidator()
        {
            RuleFor(c => c.FornecedorId).NotEqual(Guid.Empty).WithMessage("Id do fornecedor inválido");

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
