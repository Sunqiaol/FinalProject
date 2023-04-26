namespace FinalProject.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int RestaurantId { get; set; }

        public DateTime ReservationTime { get; set; }

        public string CustomerName { get; set; }

        public int Size { get; set; }

    }
}
