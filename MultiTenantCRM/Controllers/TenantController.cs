﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantCRM.Data;
using MultiTenantCRM.Models;

namespace MultiTenantCRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TenantController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            var tenants = await _context.Tenants.ToListAsync();
            return Ok(tenants);
        }

        [HttpPost]
        public async Task<IActionResult> AddTenant([FromBody] Tenant tenant)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllTenants), new { id = tenant.TenantId }, tenant);
        }
        [HttpGet("{tenantId}/users")]
        public async Task<IActionResult> GetUsersByTenant(int tenantId)
        {
            var users = await _context.Users.Where(u => u.TenantId == tenantId).ToListAsync();
            return Ok(users);
        }

    }

}