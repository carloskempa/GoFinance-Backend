using GoFinance.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace GoFinance.Domain.Interfaces.Queries
{
    public interface ICategoriaQueries
    {
        Task<Categoria> ObterPorId(Guid categoriaId, Guid usuarioId);
        IQueryable<Categoria> ObterCategoriasQuery(Guid categoriaId);
    }
}
