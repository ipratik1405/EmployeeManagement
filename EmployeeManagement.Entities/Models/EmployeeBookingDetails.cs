using System;

namespace EmployeeManagement.Entities.Models
{
    public class EmployeeBookingDetails
    {
        public int RoomBookingId { get; set; }
        public DateTime BookingStartTime { get; set; }
        public DateTime BookingEndTime { get; set; }
        public decimal Expense { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public decimal TotalExpense { get; set; }
        public string GenderName { get; set; }

    }
}
