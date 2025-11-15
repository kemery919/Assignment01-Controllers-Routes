using Emery_ChinookEndpoints.Data;
using Emery_ChinookEndpoints.Models.Entities;
using Emery_ChinookEndpoints.Models.Dtos;
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
      var customers = await _context.Customer
        .Include(c => c.SupportRep)
        .Select(c => new CustomerDto {
          Id = c.CustomerId,
          FirstName = c.FirstName,
          LastName = c.LastName,
          Company = c.Company,
          Address = c.Address,
          City = c.City,    
          State = c.State,
          Country = c.Country,
          PostalCode = c.PostalCode,
          Phone = c.Phone,
          Fax = c.Fax,
          Email = c.Email,
          SupportRepId = c.SupportRepId,
          SupportRepName = c.SupportRep.FirstName + " " + c.SupportRep.LastName
        })
        .ToListAsync();

      return Ok(customers);
    }

    // Get Customer by ID
    [HttpGet("{customerId:int}")] 
    public async Task<ActionResult<Customer>> GetCustomerById(int customerId) {
      var customer = await _context.Customer
        .Where(c => c.CustomerId == customerId)
        .Include(c => c.SupportRep)
        .Select(c => new CustomerDto {
          Id = c.CustomerId,
          FirstName = c.FirstName,
          LastName = c.LastName,
          Company = c.Company,
          Address = c.Address,
          City = c.City,    
          State = c.State,
          Country = c.Country,
          PostalCode = c.PostalCode,
          Phone = c.Phone,
          Fax = c.Fax,
          Email = c.Email,
          SupportRepId = c.SupportRepId,
          SupportRepName = c.SupportRep.FirstName + " " + c.SupportRep.LastName
        })
        .SingleOrDefaultAsync();

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