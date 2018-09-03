using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entities.Models
{
    public class RoomBooking
    {
        public int RoomBookingId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime BookingStartTime { get; set; }
        [Required]
        public DateTime BookingEndTime { get; set; }
        [Required]
        public decimal Expense { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
