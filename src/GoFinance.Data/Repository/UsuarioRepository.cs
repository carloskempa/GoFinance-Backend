using GoFinance.Data.Context;
using GoFinance.Domain.Core.Data;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoFinance.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FinanceContext _context;

        public UsuarioRepository(FinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        #region Usuario

        public async Task<Usuario> ObterPorId(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> ObterPorLogin(string login)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.Login == login);
        }

        public async Task<Usuario> ObterPorEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.Email.Endereco == email);
        }

        public async Task<Usuario> ObterPorRefreshToken(string token)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.RefleshToken == token);
        }

        public async Task<Usuario> ObterPorTokenAlterarSenha(string token)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.TokenAlteracaoSenha == token);
        }

        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }


        #endregion

        #region Categoria

        public async Task<Categoria> ObterCategoriaPorId(Guid id, Guid usuarioId)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias(Guid usuarioId)
        {
            return await _context.Categorias.AsNoTracking().Where(c => c.UsuarioId == usuarioId).ToListAsync();
        }

        public void Adicionar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
        }

        public void Deletar(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
        }


        #endregion

        #region Fornecedor

        public void Adicionar(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
        }

        public void Atualizar(Fornecedor fornecedor)
        {
            _context.Fornecedores.Update(fornecedor);
        }

        public void Deletar(Fornecedor fornecedor)
        {
            _context.Fornecedores.Remove(fornecedor);
        }

        public async Task<IEnumerable<Fornecedor>> ObterFornecedores()
        {
            return await _context.Fornecedores.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Fornecedor>> ObterFornecedores(Guid usuarioId)
        {
            return await _context.Fornecedores.AsNoTracking().Where(c => c.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<Fornecedor> ObterFornecedorPorId(Guid id, Guid usuarioId)
        {
            return await _context.Fornecedores.FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);
        }


        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
