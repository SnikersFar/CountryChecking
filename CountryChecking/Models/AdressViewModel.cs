namespace CountryChecking.Models
{
    public class AdressViewModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public int? HouseNumber { get; set; }
    }
}