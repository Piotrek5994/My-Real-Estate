namespace Core.ModelDto;

public class AddressDto
{
    public string Id { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string StreetName { get; set; }
    public string StreetNumber { get; set; }
    public int State { get; set; }
    public string PropertyId { get; set; } //FK
    public string UserId { get; set; } //FK
}