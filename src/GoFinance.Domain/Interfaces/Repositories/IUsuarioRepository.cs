using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoFinance.Domain.Core.Data;
using GoFinance.Domain.Entities;

namespace GoFinance.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        //Usuario
        Task<IEnumerable<Usuario>> ObterTodos();
        Task<Usuario> ObterPorId(Guid id);
        Task<Usuario> ObterPorLogin(string login);
        Task<Usuario> ObterPorEmail(string email);
        Task<Usuario> ObterPorRefreshToken(string token);
        Task<Usuario> ObterPorTokenAlterarSenha(string token);

        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);


        //Categoria
        Task<IEnumerable<Categoria>> ObterCategorias();
        Task<IEnumerable<Categoria>> ObterCategorias(Guid usuarioId);
        Task<Categoria> ObterCategoriaPorId(Guid id, Guid usuarioId);
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Deletar(Categoria categoria);


        //Fornecedor
        Task<IEnumerable<Fornecedor>> ObterFornecedores();
        Task<IEnumerable<Fornecedor>> ObterFornecedores(Guid usuarioId);
        Task<Fornecedor> ObterFornecedorPorId(Guid id, Guid usuarioId);
        void Adicionar(Fornecedor fornecedor);
        void Atualizar(Fornecedor fornecedor);
        void Deletar(Fornecedor fornecedor);
    }
}
