using Emery_ChinookEndpoints.Data;
using Emery_ChinookEndpoints.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emery_ChinookEndpoints.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context) {
      _context = context;
    }

    // Get All Customers
    [HttpGet("")] 
    public async Task<ActionResult<List<Customer>>> GetAllCustomers() {
      var customers = await _context.Customer.ToListAsync();

      return Ok(customers);
    }

    // Get Customer by ID
    [HttpGet("{customerId:int}")] 
    public async Task<ActionResult<Customer>> GetCustomerById(int customerId) {
      var customer = await _context.Customer.SingleOrDefaultAsync(c => c.CustomerId == customerId);

      if (customer == null) {
        return NotFound($"No customer found with the ID: {customerId}");
      }

      return Ok(customer);
    }

    // Delete Customer
    [HttpDelete("{customerId:int}")] 
    public async Task<ActionResult> DeleteCustomer(int customerId) {
      var customer = await _context.Customer.SingleOrDefaultAsync(c => c.CustomerId == customerId);

      if (customer == null) {
        return NotFound($"No customer found with the ID: {customerId}");
      }

      _context.Customer.Remove(customer);
      await _context.SaveChangesAsync();
      return NoContent();
    }

  }
}