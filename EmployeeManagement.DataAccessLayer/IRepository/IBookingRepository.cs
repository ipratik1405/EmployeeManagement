using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccessLayer.IRepository
{
    public interface IBookingRepository : IDisposable
    {
        Task<List<Room>> GetAvailableRooms(DateTime startDateTime, DateTime endDateTime);
        Task<int> RoomBooking(RoomBooking roomBooking);
        Task<bool> CheckRoomAvailability(DateTime startDateTime, DateTime endDateTime, int roomId);
        Task<List<EmployeeBookingDetails>> RoomDetails(int selectedEmployeeId);
        Task<List<Room>> Search(int capacity, bool isWifiEnabled, string screenType, DateTime startTime, DateTime endTime);
    }
}
