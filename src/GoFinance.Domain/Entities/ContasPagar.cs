using System;
using System.Collections.Generic;
using GoFinance.Domain.Core.DomainObjects;

namespace GoFinance.Domain.Entities
{
    public class ContasPagar : Entity
    {
        public ContasPagar(string nome, int numeroParcelas, decimal valorTotal, string observacoes, Guid categoriaId, Guid usuarioId, Guid fornecedorId)
        {
            Nome = nome;
            NumeroParcelas = numeroParcelas;
            ValorTotal = valorTotal;
            Observacoes = observacoes;
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;
            FornecedorId = fornecedorId;
        }

        public string Nome { get; private set; }
        public int NumeroParcelas { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string Observacoes { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid FornecedorId { get; private set; }
        public Categoria Categoria { get; private set; }
        public Usuario Usuario { get; private set; }
        public Fornecedor Fornecedor { get; private set; }

        private readonly List<Parcela> _movimentoItems;
        public IReadOnlyCollection<Parcela> MovimentoItems => _movimentoItems;

        public void AdicionarItem(Parcela item)
        {
            item.Validar();
            item.AssociarMovimento(Id);

            _movimentoItems.Add(item);
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "Nome do movimento deve ser preenchido.");
            Validacoes.ValidarSeIgual(CategoriaId,Guid.Empty, "O Campo da Categoria deve ser preenchida.");
            Validacoes.ValidarSeIgual(FornecedorId, Guid.Empty, "O Campo fornecedor deve ser preenchido.");
            Validacoes.ValidarSeIgual(UsuarioId, Guid.Empty, "O UsuarioId deve ser preenchido.");
            Validacoes.ValidarSeIgual(ValorTotal, 0, "Valor da movimentação deve ser maior que 0.");
            Validacoes.ValidarTamanho(Observacoes, 500, "O campo observações pode ter no maximo 500 caracteres.");
        }
    }
}
