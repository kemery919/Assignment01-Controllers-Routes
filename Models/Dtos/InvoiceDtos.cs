namespace Emery_ChinookEndpoints.Models.Dtos;

public class InvoiceDtos {
  public int InvoiceId { get; set; } = default!;
  public int CustomerId { get; set; } = default!;
  public string CustomerName { get; set; } = default!;
  public DateTime InvoiceDate { get; set; } = default!;
  public string BillingAddress { get; set; } = default!;
  public string BillingCity { get; set; } = default!;
  public string BillingState { get; set; } = default!;
  public string BillingCountry { get; set; } = default!;
  public string BillingPostalCode { get; set; } = default!;
  public decimal Total { get; set; } = default!;
}