namespace FinalProject.Models
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string statusDescription { get; set; }

        public List<Reservation> reservations { get; set; } = new();

        public List<Restaurant> restaurants { get; set;} = new();
    }
}
