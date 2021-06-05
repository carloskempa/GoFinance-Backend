using System;
using GoFinance.Domain.Core.DomainObjects;
using GoFinance.Domain.Core.ValuesObjects;

namespace GoFinance.Domain.Entities
{
    public class Fornecedor : Entity
    {
        public string Nome { get; private set; }
        public CnpjCpf Cnpj { get; private set; }
        public string UrlSite { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }

        public Fornecedor(string nome, string urlSite, string descricao, bool ativo, Guid usuarioId)
        {
            Nome = nome;
            UrlSite = urlSite;
            Descricao = descricao;
            Ativo = ativo;
            UsuarioId = usuarioId;
        }
        protected Fornecedor() { }
        public override string ToString()
        {
            return $"{ Cnpj} { Nome }";
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome da Fornecedor não pode estar vazio");
            Validacoes.ValidarSeIgual(UsuarioId, Guid.Empty, "O campo UsuarioId da fornecedor não pode ser vazio");
        }

    }
}
