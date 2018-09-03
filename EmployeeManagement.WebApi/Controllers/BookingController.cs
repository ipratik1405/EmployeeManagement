using EmployeeManagement.BusinessLayer;
using EmployeeManagement.BusinessLayer.Services;
using EmployeeManagement.Entities.AppSettings;
using EmployeeManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Booking")]
    public class BookingController : Controller
    {
        public AppSettings _appSettings;
        private readonly IServices<BookingService> _bookingService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IServices<BookingService> bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all available rooms
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Room))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Get([FromQuery]DateTime startTime, [FromQuery]DateTime endTime)
        {
            _logger.LogDebug("Getting all available rooms" + " startTime" + startTime + " endTime" + endTime);
            var roomDetails = await _bookingService.Service.GetAvailableRooms(startTime, endTime);
            if (roomDetails == null || roomDetails.Count == 0)
            {
                _logger.LogDebug("Getting all available rooms Failed" + " startTime" + startTime + " endTime" + endTime);
                return NoContent();
            }
            _logger.LogDebug("Getting all available rooms" + " startTime" + startTime + " endTime" + endTime);
            return Ok(roomDetails);
        }

        /// <summary>
        /// books a available room
        /// </summary>
        /// <param name="roomBooking"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Roombooking Started");
                var bookingId = await _bookingService.Service.RoomBooking(roomBooking);
                if (bookingId != 0)
                {
                    return Ok(bookingId);
                }
                _logger.LogDebug("RoomBooking Failed");
                return BadRequest(bookingId);
            }
            return BadRequest();
        }

        /// <summary>
        /// Checks if a particular room is available
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("{roomId:int}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get([FromQuery]DateTime startTime, [FromQuery]DateTime endTime, int roomId)
        {
            _logger.LogDebug("checking a available rooms" + " startTime" + startTime + " endTime" + endTime + "roomId" + roomId);
            var isNotAvailable = await _bookingService.Service.CheckRoomAvailability(startTime, endTime, roomId);
            if (isNotAvailable == true)
            {
                return BadRequest(isNotAvailable);
            }
            _logger.LogDebug("checking a available rooms Succeed" + " startTime" + startTime + " endTime" + endTime + "roomId" + roomId);
            return Accepted(isNotAvailable);
        }

        /// <summary>
        /// Returns roomexpense details of all/selected employee
        /// </summary>
        /// <param name="selectedEmployeeId"></param>
        /// <returns></returns>
        [HttpGet("RoomDetails/{selectedEmployeeId:int}")]
        [ProducesResponseType(200, Type = typeof(EmployeeBookingDetailsView))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Get(int selectedEmployeeId)
        {
            _logger.LogDebug("Getting all available rooms" + "selectedEmployeeId" + selectedEmployeeId);
            var details = await _bookingService.Service.RoomDetails(selectedEmployeeId);
            if (details == null)
            {
                return NoContent();
            }
            _logger.LogDebug("Getting all available rooms" + "selectedEmployeeId" + selectedEmployeeId);
            return Ok(details);
        }

        /// <summary>
        /// Returns All available rooms with search criteria
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="isWifiEnabled"></param>
        /// <param name="screenSize"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet("{capacity:int}/{isWifiEnabled:bool}/{screenSize}")]
        [ProducesResponseType(200, Type = typeof(Room))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Get(int capacity, bool isWifiEnabled, string screenType, [FromQuery]DateTime startTime, [FromQuery]DateTime endTime)
        {
            _logger.LogDebug("Getting all available rooms with search criteria" + " startTime" + startTime + " endTime" + endTime + "screenType" + screenType + "iswifienabled" + isWifiEnabled);
            var roomDetails = await _bookingService.Service.Search(capacity, isWifiEnabled, screenType, startTime, endTime);
            if (roomDetails != null)
            {
                return Ok(roomDetails);
            }
            _logger.LogDebug("Getting all available rooms  with search criteria" + " startTime" + startTime + " endTime" + endTime + "screenType" + screenType + "iswifienabled" + isWifiEnabled);
            return NoContent();
        }
    }
}