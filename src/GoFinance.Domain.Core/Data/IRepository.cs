using System;
using GoFinance.Domain.Core.DomainObjects;

namespace GoFinance.Domain.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
