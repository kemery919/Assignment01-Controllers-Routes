namespace Emery_ChinookEndpoints.Models.Dtos;

public class CustomerDto
{
  public int Id { get; set; } = default!;
  public string FirstName { get; set; } = default!;
  public string LastName { get; set; } = default!;
  public string Company { get; set; } = default!;
  public string Address { get; set; } = default!;
  public string City { get; set; } = default!;
  public string State { get; set; } = default!;
  public string Country { get; set; } = default!;
  public string PostalCode { get; set; } = default!;
  public string Phone { get; set; } = default!;
  public string Fax { get; set; } = default!;
  public string Email { get; set; } = default!;
  public int? SupportRepId { get; set; } = default!;
  public string SupportRepName { get; set; } = default!;
}