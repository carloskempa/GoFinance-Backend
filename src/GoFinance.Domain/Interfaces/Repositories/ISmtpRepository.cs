using GoFinance.Domain.Core.Data;
using GoFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoFinance.Domain.Interfaces.Repositories
{
    public interface ISmtpRepository : IRepository<Smtp>
    {
        Task<IEnumerable<Smtp>> ObterTodos();
        Task<Smtp> ObterPorId(Guid id);
        Task<Smtp> ObterAtivo();

        void Adicionar(Smtp smtp);
        void Atualizar(Smtp smtp);
        void Deletar(Smtp smtp);
    }
}
