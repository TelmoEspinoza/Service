using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Data;
using Service.Models;
namespace Service.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CustomerController : ControllerBase
    {
        private readonly MilkStoreContext _context;

        public CustomerController(MilkStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

       
        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
        }
        [HttpPost]
        public IActionResult UpdateCustomer(int id, Customer updatedCustomer)
        {
            var customer = _context.Customers.Find(id);
            var notFoundResult = new NotFoundObjectResult(new { Message = $"Customer with ID {id} not found." });
            if (customer == null)
            {
                return NotFound(notFoundResult);
            }

            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            customer.Address = updatedCustomer.Address;

            _context.SaveChanges();
            return Ok(customer);

        }
        [HttpPost]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            var notFoundResult = new NotFoundObjectResult(new { Message = $"Customer with ID {id} not found." });
            if (customer == null)
            {
                return NotFound(notFoundResult);
            }
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return Ok(customer);
            }
        }

    }
}
