using Emery_ChinookEndpoints.Data;
using Emery_ChinookEndpoints.Models.Entities;
using Emery_ChinookEndpoints.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emery_ChinookEndpoints.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class InvoiceController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public InvoiceController(ApplicationDbContext context) {
      _context = context;
    }

    // Get All Invoices
    [HttpGet("")] 
    public async Task<ActionResult<List<Invoice>>> GetAllInvoices() {
      var invoices = await _context.Invoice
        .Include(invoice => invoice.Customer)
        .ToListAsync();

      return Ok(invoices);
    }
    
    // Get Invoice by ID
    [HttpGet("{invoiceId:int}")] 
    public async Task<ActionResult<Invoice>> GetInvoiceById(int invoiceId) {
      var invoice = await _context.Invoice
        .Include(invoice => invoice.Customer)
        .SingleOrDefaultAsync(invoice => invoice.InvoiceId == invoiceId);

      if (invoice == null) {
        return NotFound($"No invoice found with the ID: {invoiceId}");
      }

      return Ok(invoice);
    }

    // Delete Invoice
    [HttpDelete("{invoiceId:int}")] 
    public async Task<ActionResult> DeleteInvoice(int invoiceId) {
      var invoice = await _context.Invoice.SingleOrDefaultAsync(invoice => invoice.InvoiceId == invoiceId);

      if (invoice == null) {
        return NotFound($"No invoice found with the ID: {invoiceId}");
      }

      _context.Invoice.Remove(invoice);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpGet("stats")]
    public async Task<ActionResult<List<Invoice>>> GetInvoiceStats(int topNumExpensiveInvoices) {
      if (topNumExpensiveInvoices <= 0) {
        return BadRequest("Number of invoices must be greater than 0");
      }

      var invoices = await _context.Invoice
        .Include(invoice => invoice.Customer)
        .ToListAsync();
      
      var groupedInvoices = invoices
        .OrderByDescending(invoice => (double)invoice.Total)
        .GroupBy(invoice => invoice.BillingState ?? "No Billing State")
        .Select(group => group.Take(topNumExpensiveInvoices))
        .ToList();

      return Ok(groupedInvoices);
    }

    // This is commented out because it is the same as the one above but it does not work correctly

    // [HttpGet("stats2")]
    // public async Task<ActionResult<List<Invoice>>> GetInvoiceStats2(int topNumExpensiveInvoices) {
    //   if (topNumExpensiveInvoices <= 0) {
    //     return BadRequest("Number of invoices must be greater than 0");
    //   }
      
    //   var invoices = await _context.Invoice
    //     .OrderByDescending(invoice => (double)invoice.Total)
    //     .GroupBy(invoice => invoice.BillingState ?? "No Billing State")
    //     .Select(group => group.Take(topNumExpensiveInvoices))
    //     .ToListAsync();

    //   return Ok(invoices);
    // }

  }
}