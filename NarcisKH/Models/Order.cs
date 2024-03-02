namespace NarcisKH.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public CityProvinces CityProvince { get; set; }
        public Status Status { get; set; }
        public User Employee { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
