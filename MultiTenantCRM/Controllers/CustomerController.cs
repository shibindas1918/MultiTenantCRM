using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantCRM.Data;
using MultiTenantCRM.Models;

namespace MultiTenantCRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetCustomersByTenant(int tenantId)
        {
            var customers = await _context.Contacts.Where(c => c.TenantId == tenantId).ToListAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Contact customer)
        {
            _context.Contacts.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomersByTenant), new { id = customer.ContactId }, customer);
        }
    }

}
