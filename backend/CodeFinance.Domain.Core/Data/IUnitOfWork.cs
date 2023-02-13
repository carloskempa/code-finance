using System.Threading.Tasks;

namespace CodeFinance.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
