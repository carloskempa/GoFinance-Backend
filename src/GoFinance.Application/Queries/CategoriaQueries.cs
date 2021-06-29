using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Queries;
using GoFinance.Domain.Interfaces.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoFinance.Application.Queries
{
    public class CategoriaQueries : ICategoriaQueries
    {

        private readonly IUsuarioRepository _usuarioRepository;
        public CategoriaQueries(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IQueryable<Categoria> ObterCategoriasQuery(Guid usuarioId)
        {
            return _usuarioRepository.ObterCategorias().Where(c => c.UsuarioId == usuarioId && c.Ativo == true);
        }

        public async Task<Categoria> ObterPorId(Guid categoriaId, Guid usuarioId)
        {
            return await _usuarioRepository.ObterCategoriaPorId(categoriaId, usuarioId);
        }
    }
}
