using Semana10Sales.DOMAIN.Core.Entities;

namespace Semana10Sales.DOMAIN.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<Customer>> GetAll();
        IEnumerable<Customer> GetAllBySP();
        Task<Customer> GetById(int id);
        Task<bool> Insert(Customer customer);
        Task<bool> Update(Customer customer);
    }
}