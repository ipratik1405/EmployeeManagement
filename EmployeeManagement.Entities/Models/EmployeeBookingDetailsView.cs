using System;

namespace EmployeeManagement.Entities.Models
{
    public class EmployeeBookingDetailsView
    {
        public int RoomBookingId { get; set; }
        public DateTime BookingStartTime { get; set; }
        public DateTime BookingEndTime { get; set; }
        public decimal Expense { get; set; }
        public string Title { get; set; }
        public Employee Employee { get; set; }
        public decimal TotalExpense { get; set; }
    }
}
