using EmployeeManagement.BusinessLayer;
using EmployeeManagement.BusinessLayer.Services;
using EmployeeManagement.Entities.AppSettings;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.WebApi.Test.Controllers
{
    public class BookingControllerTest
    {
        [Fact]
        public void GetAvailableRooms()
        {
            var availableRooms = ObjectBuilder.GetAvailableRooms();
            var bookingService = new Mock<IServices<BookingService>>();
            var mockBookingService = new Mock<BookingService>(null);
            var logger = new Mock<ILogger<BookingController>>();

            mockBookingService.Setup(z => z.GetAvailableRooms(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(availableRooms));
            bookingService.Setup(z => z.Service).Returns(mockBookingService.Object);
            var bookingController = new BookingController(bookingService.Object, logger.Object);

            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = DateTime.UtcNow.AddHours(5);

            var result = bookingController.Get(startTime, endTime);
            Assert.NotNull(result.Result);
            OkObjectResult okObjectResult = result.Result as OkObjectResult;
            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Fact]
        public void GetAvailableRooms_Negative()
        {
            var bookingService = new Mock<IServices<BookingService>>();
            var mockBookingService = new Mock<BookingService>(null);
            var logger = new Mock<ILogger<BookingController>>();

            mockBookingService.Setup(z => z.GetAvailableRooms(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult<List<Room>>(null));
            bookingService.Setup(z => z.Service).Returns(mockBookingService.Object);
            var bookingController = new BookingController(bookingService.Object, logger.Object);

            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = DateTime.UtcNow.AddHours(5);

            var result = bookingController.Get(startTime, endTime);
            Assert.NotNull(result.Result);
            NoContentResult okObjectResult = result.Result as NoContentResult;
            Assert.Equal(204, okObjectResult.StatusCode);
        }

        [Fact]
        public void CheckRoomAvailability_Positive()
        {
            var bookingService = new Mock<IServices<BookingService>>();
            var mockBookingService = new Mock<BookingService>(null);
            var logger = new Mock<ILogger<BookingController>>();

            mockBookingService.Setup(z => z.CheckRoomAvailability(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(Task.FromResult(false));
            bookingService.Setup(z => z.Service).Returns(mockBookingService.Object);
            var bookingController = new BookingController(bookingService.Object, logger.Object);

            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = DateTime.UtcNow.AddHours(5);

            var result = bookingController.Get(startTime, endTime, 1);
            Assert.NotNull(result.Result);
            AcceptedResult okObjectResult = result.Result as AcceptedResult;
            Assert.Equal(202, okObjectResult.StatusCode);
        }

        [Fact]
        public void CheckRoomAvailability_Negative()
        {
            var bookingService = new Mock<IServices<BookingService>>();
            var mockBookingService = new Mock<BookingService>(null);
            var logger = new Mock<ILogger<BookingController>>();

            mockBookingService.Setup(z => z.CheckRoomAvailability(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            bookingService.Setup(z => z.Service).Returns(mockBookingService.Object);
            var bookingController = new BookingController(bookingService.Object, logger.Object);

            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = DateTime.UtcNow.AddHours(5);

            var result = bookingController.Get(startTime, endTime, 1);
            Assert.NotNull(result.Result);
            BadRequestObjectResult okObjectResult = result.Result as BadRequestObjectResult;
            Assert.Equal(400, okObjectResult.StatusCode);
        }

        [Fact]
        public void RoomDetailsByEmployee()
        {
            var bookingService = new Mock<IServices<BookingService>>();
            var mockBookingService = new Mock<BookingService>(null);
            var logger = new Mock<ILogger<BookingController>>();

            mockBookingService.Setup(z => z.RoomDetails(It.IsAny<int>())).Returns(Task.FromResult<List<EmployeeBookingDetailsView>>(null));
            bookingService.Setup(z => z.Service).Returns(mockBookingService.Object);
            var bookingController = new BookingController(bookingService.Object, logger.Object);

            var result = bookingController.Get(1);
            Assert.NotNull(result.Result);
            NoContentResult okObjectResult = result.Result as NoContentResult;
            Assert.Equal(204, okObjectResult.StatusCode);
        }

        [Fact]
        public void RoomDetailsByEmployee_Positive()
        {
            var bookingService = new Mock<IServices<BookingService>>();
            var mockBookingService = new Mock<BookingService>(null);
            var logger = new Mock<ILogger<BookingController>>();
            var employeeBookingDetails = ObjectBuilder.EmployeeBookingDetails();
            mockBookingService.Setup(z => z.RoomDetails(It.IsAny<int>())).Returns(Task.FromResult(employeeBookingDetails));
            bookingService.Setup(z => z.Service).Returns(mockBookingService.Object);
            var bookingController = new BookingController(bookingService.Object, logger.Object);
        
            var result = bookingController.Get(1);
            Assert.NotNull(result.Result);
            OkObjectResult okObjectResult = result.Result as OkObjectResult;
            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Fact]
        public void Booking_Positive()
        {
            var bookingService = new Mock<IServices<BookingService>>();
            var mockBookingService = new Mock<BookingService>(null);
            var logger = new Mock<ILogger<BookingController>>();
            var roombooking = ObjectBuilder.RoomBooking();
            mockBookingService.Setup(z => z.RoomBooking(It.IsAny<RoomBooking>())).Returns(Task.FromResult(1));
            bookingService.Setup(z => z.Service).Returns(mockBookingService.Object);
            var bookingController = new BookingController(bookingService.Object, logger.Object);

            var result = bookingController.Post(roombooking);
            Assert.NotNull(result.Result);
            OkObjectResult okObjectResult = result.Result as OkObjectResult;
            Assert.Equal(200, okObjectResult.StatusCode);
        }

       
    }
}