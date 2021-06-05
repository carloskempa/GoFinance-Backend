using System;
using System.Collections.Generic;
using GoFinance.Domain.Core.DomainObjects;

namespace GoFinance.Domain.Entities
{
    public class Categoria : Entity
    {
        public string Nome { get; private set; }
        public int Codigo { get; private set; }
        public bool Ativo { get; private set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; private set; }
        public ICollection<ContasPagar> ContasPagar { get; set; }


        public Categoria(string nome, int codigo, bool ativo, Guid usuarioId)
        {
            Nome = nome;
            Codigo = codigo;
            Ativo = ativo;
            UsuarioId = usuarioId;
        }
        protected Categoria() { }


        public void Ativar() => Ativo = true;
        public void Inativar() => Ativo = false;

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome da Categoria não pode estar vazio");
            Validacoes.ValidarSeIgual(Codigo, 0, "O campo Código da Categoria não pode estar vazio");
            Validacoes.ValidarSeIgual(UsuarioId, Guid.Empty, "O campo UsuarioId da categoria não pode ser vazio");
        }

        public override string ToString()
        {
            return $"{this.Codigo} - {this.Nome}";
        }

    }
}
