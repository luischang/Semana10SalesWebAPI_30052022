using Microsoft.EntityFrameworkCore;
using Semana10Sales.DOMAIN.Core.Entities;
using Semana10Sales.DOMAIN.Core.Interfaces;
using Semana10Sales.DOMAIN.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana10Sales.DOMAIN.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SalesContext _context;

        public CustomerRepository(SalesContext context)
        {
            _context = context;
        }

        //Get All Customers
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customer.ToListAsync();
        }
        //Get Customer by id
        public async Task<Customer> GetById(int id)
        {
            return await _context.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Get All Customers by Stored Procedure
        public IEnumerable<Customer> GetAllBySP()
        {
            return _context.Customer.FromSqlInterpolated($"EXECUTE dbo.GET_ALL_CUSTOMER").ToList();
        }

        //Insert Customer
        public async Task<bool> Insert(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        //Update Customer
        public async Task<bool> Update(Customer customer)
        {
            _context.Customer.Update(customer);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
        //Delete Customer
        public async Task<bool> Delete(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            //Validate is null
            if (customer == null)
                return false;

            _context.Customer.Remove(customer);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;

        }
    }
}
