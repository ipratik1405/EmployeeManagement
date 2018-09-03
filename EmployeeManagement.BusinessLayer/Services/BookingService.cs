using EmployeeManagement.BusinessLayer.IServices;
using EmployeeManagement.DataAccess;
using EmployeeManagement.DataAccessLayer.Repository;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.BusinessLayer.Services
{
    public class BookingService : IBookingService
    {
        public IDatabase<BookingRepository> _bookingRepository;
        public BookingService(IDatabase<BookingRepository> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bookingRepository.Repository?.Dispose();
            }
        }

        /// <summary>
        /// Gets all available rooms
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public virtual async Task<List<Room>> GetAvailableRooms(DateTime startDateTime, DateTime endDateTime)
        {
            var availableRooms = await _bookingRepository.Repository.GetAvailableRooms(startDateTime, endDateTime);
            return availableRooms;
        }

        /// <summary>
        /// Checks if a room is available
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public virtual async Task<bool> CheckRoomAvailability(DateTime startDateTime, DateTime endDateTime, int roomId)
        {
            var isNotAvailable = await _bookingRepository.Repository.CheckRoomAvailability(startDateTime, endDateTime, roomId);
            return isNotAvailable;
        }

        /// <summary>
        /// Books a room 
        /// </summary>
        /// <param name="roomBooking"></param>
        /// <returns>Bookingid</returns>
        public virtual async Task<int> RoomBooking(RoomBooking roomBooking)
        {
            return await _bookingRepository.Repository.RoomBooking(roomBooking);
        }

        /// <summary>
        /// Returns roomexpense details of all/selected employee 
        /// </summary>
        /// <param name="selectedEmployeeId"></param>
        /// <returns></returns>
        public virtual async Task<List<EmployeeBookingDetailsView>> RoomDetails(int selectedEmployeeId)
        {
            var exployeeExpenseReport = await _bookingRepository.Repository.RoomDetails(selectedEmployeeId);
            List<EmployeeBookingDetailsView> employeeBookingDetails = new List<EmployeeBookingDetailsView>();
            foreach (var item in exployeeExpenseReport)
            {
                EmployeeBookingDetailsView bookingDetailsView = new EmployeeBookingDetailsView()
                {
                    BookingEndTime = item.BookingEndTime,
                    BookingStartTime = item.BookingStartTime,
                    Expense = item.Expense,
                    RoomBookingId = item.RoomBookingId,
                    Title = item.Title,
                    TotalExpense = item.TotalExpense,
                    Employee = new Employee()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        MiddleName = item.MiddleName,
                        Gender = new Gender()
                        {
                            Name = item.GenderName
                        }
                    }
                };
                employeeBookingDetails.Add(bookingDetailsView);
            }
            return employeeBookingDetails;
        }

        /// <summary>
        /// Seraches room with certain criteria
        /// </summary>
        /// <param name="seatSize"></param>
        /// <param name="isWifiEnabled"></param>
        /// <param name="screenType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual async Task<List<Room>> Search(int seatSize, bool isWifiEnabled, string screenType, DateTime startTime, DateTime endTime)
        {
            return await _bookingRepository.Repository.Search(seatSize, isWifiEnabled, screenType, startTime, endTime);
        }
    }
}
