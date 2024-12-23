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
        //Method to get tenant by id 
        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetCustomersByTenant(int tenantId)
        {
            var customers = await _context.Contacts.Where(c => c.TenantId == tenantId).ToListAsync();
            return Ok(customers);
        }
        //Method to create a customer 
        [HttpPost]
        public async Task<IActionResult> AddCustomer( Contact customer)
        {
            _context.Contacts.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomersByTenant), new { id = customer.ContactId }, customer);
        }
    }

}
