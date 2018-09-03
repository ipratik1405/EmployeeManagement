using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.BusinessLayer.IServices
{
    public interface IBookingService : IDisposable
    {
        Task<List<Room>> GetAvailableRooms(DateTime startDateTime, DateTime endDateTime);
        Task<bool> CheckRoomAvailability(DateTime startDateTime, DateTime endDateTime, int roomId);
        Task<int> RoomBooking(RoomBooking roomBooking);
        Task<List<EmployeeBookingDetailsView>> RoomDetails(int selectedEmployeeId);
        Task<List<Room>> Search(int seatSize, bool isWifiEnabled, string screenType, DateTime startTime, DateTime endTime);
    }
}
