namespace EmployeeManagement.Entities.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal BookingRate { get; set; }
        public int Capacity { get; set; }
        public bool IsWifiAvailable { get; set; }
        public int ScreenDisplay { get; set; }
    }
}
