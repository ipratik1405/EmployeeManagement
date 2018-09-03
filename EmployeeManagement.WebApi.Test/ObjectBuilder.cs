using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.WebApi.Test
{
    public static class ObjectBuilder
    {
        public static List<Room> GetAvailableRooms()
        {
            List<Room> roomList = new List<Room>();

            Room room = new Room
            {
                Id = 1,
                RoomNumber = "A101",
                BookingRate = 2,
                Capacity = 20,
                IsWifiAvailable = true,
                ScreenDisplay = 22
            };
            roomList.Add(room);
            return roomList;
        }

        public static List<EmployeeBookingDetailsView> EmployeeBookingDetails()
        {
            List<EmployeeBookingDetailsView> employeeBookingDetails = new List<EmployeeBookingDetailsView>();

            EmployeeBookingDetailsView employeeBookingDetail = new EmployeeBookingDetailsView
            {
                BookingEndTime = DateTime.UtcNow.AddHours(1),
                BookingStartTime = DateTime.UtcNow,
                Employee = new Employee()
                {
                    FirstName = "Joe",
                    Gender = new Gender()
                    {
                        Id = 1,
                        Name = "Male"
                    },
                    LastName = "Djo",
                    MiddleName = "Novan"
                }
            };
            employeeBookingDetails.Add(employeeBookingDetail);
            return employeeBookingDetails;
        }

        public static RoomBooking RoomBooking()
        {
            RoomBooking roomBooking = new RoomBooking
            {
                BookingEndTime = DateTime.UtcNow.AddHours(1),
                BookingStartTime = DateTime.UtcNow,
                EmployeeId = 1,
                Expense = 2,
                RoomBookingId = 0,
                RoomId = 1,
                Title = "Event desc"
            };
            return roomBooking;
        }

        public static RoomBooking RoomBooking_Negative()
        {
            RoomBooking roomBooking = new RoomBooking
            {
                BookingEndTime = DateTime.UtcNow.AddHours(1),
                BookingStartTime = DateTime.UtcNow,
                EmployeeId = 0,
                Expense = 0,
                RoomBookingId = 0,
                RoomId = 1
            };
            return roomBooking;
        }
    }
}
