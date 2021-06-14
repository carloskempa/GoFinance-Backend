using GoFinance.Application.Validators;
using GoFinance.Domain.Core.Messages;
using System;

namespace GoFinance.Application.Commands
{
    public class AdicionarFornecedorCommand : Command
    {
        public AdicionarFornecedorCommand(string nome, string cnpjCpf, string urlSite, string descricao, bool ativo, Guid usuarioId)
        {
            Nome = nome;
            CnpjCpf = cnpjCpf;
            UrlSite = urlSite;
            Descricao = descricao;
            Ativo = ativo;
            UsuarioId = usuarioId;
        }

        public string Nome { get; private set; }
        public string CnpjCpf { get; private set; }
        public string UrlSite { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public Guid UsuarioId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarFornecedorValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
