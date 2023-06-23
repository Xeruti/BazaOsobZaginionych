namespace BazaOsobZaginionych.Models
{
    public class UpdatePersonViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
