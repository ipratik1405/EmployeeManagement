using Dapper;
using EmployeeManagement.DataAccessLayer.Constants;
using EmployeeManagement.DataAccessLayer.Infrastructure;
using EmployeeManagement.DataAccessLayer.IRepository;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccessLayer.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDataBaseConnection _db;
        public BookingRepository(IDataBaseConnection db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all available rooms
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public Task<List<Room>> GetAvailableRooms(DateTime startDateTime, DateTime endDateTime)
        {
            var spName = Constant.RoomAvailablity;
            return Task.Factory.StartNew(() =>
            {
                return _db.Connection.Query<Room>(spName, new { StartTime = startDateTime, EndTime = endDateTime }, commandType: CommandType.StoredProcedure).ToList();
            });
        }

        /// <summary>
        /// Books a room 
        /// </summary>
        /// <param name="roomBooking"></param>
        /// <returns>BookingId</returns>
        public Task<int> RoomBooking(RoomBooking roomBooking)
        {
            var spName = Constant.RoomBooking;
            return Task.Factory.StartNew(() =>
            {
                return _db.Connection.ExecuteScalar<int>(spName, roomBooking, commandType: CommandType.StoredProcedure);
            });
        }

        /// <summary>
        /// Checks if a room is available
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public Task<bool> CheckRoomAvailability(DateTime startDateTime, DateTime endDateTime, int roomId)
        {
            var spName = Constant.CheckAvailability;
            return Task.Factory.StartNew(() =>
            {
                return _db.Connection.Query<bool>(spName, new { StartTime = startDateTime, EndTime = endDateTime, RoomId = roomId }, commandType: CommandType.StoredProcedure).SingleOrDefault();
            });
        }

        /// <summary>
        /// Returns roomexpense details of all/selected employee
        /// </summary>
        /// <param name="selectedEmployeeId"></param>
        /// <returns></returns>
        public Task<List<EmployeeBookingDetails>> RoomDetails(int selectedEmployeeId)
        {
            var spName = Constant.RoomDetails;
            return Task.Factory.StartNew(() =>
            {
                return _db.Connection.Query<EmployeeBookingDetails>(spName, new { SelectedEmployeeId = selectedEmployeeId }, commandType: CommandType.StoredProcedure).ToList();
            });
        }

        /// <summary>
        /// Returns All available rooms with search criteria
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="isWifiEnabled"></param>
        /// <param name="screenType"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public Task<List<Room>> Search(int capacity, bool isWifiEnabled, string screenType, DateTime startTime, DateTime endTime)
        {
            var spName = Constant.RoomSearch;
            return Task.Factory.StartNew(() =>
            {
                return _db.Connection.Query<Room>(spName, new { capacity, isWifiEnabled, screenType, startTime, endTime }, commandType: CommandType.StoredProcedure).ToList();
            });
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
                _db.Connection?.Dispose();
            }
        }
    }
}
