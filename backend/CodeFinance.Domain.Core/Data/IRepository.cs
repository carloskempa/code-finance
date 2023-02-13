using CodeFinance.Domain.Core.DomainObjects;
using System;

namespace CodeFinance.Domain.Core.Data
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        IUnitOfWork IUnitOfWork { get; }
    }
}
