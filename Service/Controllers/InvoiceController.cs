using Microsoft.AspNetCore.Mvc;
using Service.Data;
namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly MilkStoreContext _context;

        public InvoiceController(MilkStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetInvoices()
        {
            var invoices = _context.Invoices.ToList();
            return Ok(invoices);
        }

       
    }
}