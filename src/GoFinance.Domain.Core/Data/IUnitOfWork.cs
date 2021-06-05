using System.Threading.Tasks;

namespace GoFinance.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
