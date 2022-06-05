using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Semana10Sales.DOMAIN.Core.DTOs;
using Semana10Sales.DOMAIN.Core.Entities;
using Semana10Sales.DOMAIN.Core.Interfaces;

namespace Semana10Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // GET: api/Customer
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepository.GetAll();
            var customersList = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            
            return Ok(customersList);
        }

        // GET: api/Customer/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerRepository.GetById(id);
            //Validated is null
            if(customer == null)
                return NotFound();
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDTO);
        }
        // GET: api/Customer/GetByIdQueryParams?id=5
        [HttpGet("GetByIdQueryParams")]
        public async Task<IActionResult> GetByIdQueryParams([FromQuery] int id)
        {
            var customer = await _customerRepository.GetById(id);
            //Validated is null
            if (customer == null)
                return NotFound();
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDTO);
        }

        // POST: api/Customer/Insert
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] CustomerPostDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            var result = await _customerRepository.Insert(customer);
            return Ok(result);
        }

        // PUT: api/Customer/Update/{id}
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] CustomerDTO customerDTO)
        {
            if (id != customerDTO.Id)
                return BadRequest();
            var customer = _mapper.Map<Customer>(customerDTO);
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
