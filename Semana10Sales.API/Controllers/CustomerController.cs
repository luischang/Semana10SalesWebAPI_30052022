using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Semana10Sales.DOMAIN.Core.Entities;
using Semana10Sales.DOMAIN.Core.Interfaces;

namespace Semana10Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepository.GetAll();
            return Ok(customers);
        }

        // GET: api/Customer/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerRepository.GetById(id);
            //Validated is null
            if(customer == null)
                return NotFound();
            
            return Ok(customer);
        }
        // GET: api/Customer/GetByIdQueryParams?id=5
        [HttpGet("GetByIdQueryParams")]
        public async Task<IActionResult> GetByIdQueryParams([FromQuery] int id)
        {
            var customer = await _customerRepository.GetById(id);
            //Validated is null
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST: api/Customer/Insert
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Customer customer)
        {
            var result = await _customerRepository.Insert(customer);
            return Ok(result);
        }

        // PUT: api/Customer/Update/{id}
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            var result = await _customerRepository.Update(customer);
            return Ok(result);
        }

        // DELETE: api/Customer/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
                return NotFound();
            var result = await _customerRepository.Delete(id);
            return Ok(result);
        }

    }
}
