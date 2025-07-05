using HotelBooking.Domain.IRepositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class BookingController : Controller
    {

        private readonly IBookingRepo _bookingRepo;
        private readonly IHotelRepo _hotelRepo;
        private readonly IRoomRepo _roomRepo;
        private readonly ISeasonRepo _seasonRepo;

        public BookingController(IBookingRepo bookingRepo, IHotelRepo hotelRepo, IRoomRepo roomRepo, ISeasonRepo seasonRepo)
        {
            _bookingRepo = bookingRepo;
            _hotelRepo = hotelRepo;
            _roomRepo = roomRepo;
            _seasonRepo = seasonRepo;
        }
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return View(bookings);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Hotels = await _hotelRepo.GetAllAsync();
            ViewBag.Rooms = await _roomRepo.GetAllAsync();
            var today = DateTime.Today;
            var booking = new Booking
            {
                CheckInDate = today,
                CheckOutDate = today.AddDays(1)
            };
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (booking.CheckInDate < DateTime.Today)
            {
                ModelState.AddModelError("CheckInDate", "Check-in date cannot be in the past.");
            }

            if (booking.CheckOutDate < DateTime.Today)
            {
                ModelState.AddModelError("CheckOutDate", "Check-out date cannot be in the past.");
            }

            if (booking.CheckOutDate <= booking.CheckInDate)
            {
                ModelState.AddModelError("CheckOutDate", "Check-out date must be after check-in date.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Hotels = await _hotelRepo.GetAllAsync();
                ViewBag.Rooms = await _roomRepo.GetAllAsync();
                return View(booking);
            }

            var existingBookings = await _bookingRepo.GetBookingsByRoomIdAsync(booking.RoomId ?? 0);
            var isRoomAvailable = !existingBookings.Any(b =>
                booking.CheckInDate < b.CheckOutDate && booking.CheckOutDate > b.CheckInDate);

            if (!isRoomAvailable)
            {
                ModelState.AddModelError("RoomId", "Room is not available in the selected period.");
                ViewBag.Hotels = await _hotelRepo.GetAllAsync();
                ViewBag.Rooms = await _roomRepo.GetAllAsync();
                return View(booking);
            }

       
            var room = await _roomRepo.GetByIdAsync(booking.RoomId ?? 0);
            if (room == null)
            {
                ModelState.AddModelError("RoomId", "Room not found.");
                ViewBag.Hotels = await _hotelRepo.GetAllAsync();
                ViewBag.Rooms = await _roomRepo.GetAllAsync();
                return View(booking);
            }

            var season = await _seasonRepo.GetSeasonByDateRangeAsync(booking.CheckInDate, booking.CheckOutDate);
            decimal basePrice = room.PricePerNight;
            decimal adjustedPrice = season != null ? basePrice + (basePrice * season.PriceFactor) : basePrice;

            var nights = (booking.CheckOutDate - booking.CheckInDate).Days;
            booking.TotalPrice = adjustedPrice * nights;
            booking.AgentId = 2; // add any agent in your db manually and set the id here modified later

            await _bookingRepo.AddAsync(booking);
            await _bookingRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            ViewBag.Hotels = await _hotelRepo.GetAllAsync();
            ViewBag.Rooms = await _roomRepo.GetAllAsync();
            return View(booking);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Hotels = await _hotelRepo.GetAllAsync();
                ViewBag.Rooms = await _roomRepo.GetAllAsync();
                return View(booking);
            }

            var existing = await _bookingRepo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Check availability
            var existingBookings = await _bookingRepo.GetBookingsByRoomIdAsync(booking.RoomId ?? 0);
            var isRoomAvailable = !existingBookings.Any(b =>
                booking.CheckInDate < b.CheckOutDate && booking.CheckOutDate > b.CheckInDate && b.Id != booking.Id);

            if (!isRoomAvailable)
            {
                ModelState.AddModelError("RoomId", "Room is not available in the selected period.");
                ViewBag.Hotels = await _hotelRepo.GetAllAsync();
                ViewBag.Rooms = await _roomRepo.GetAllAsync();
                return View(booking);
            }

            // Update properties manually
            existing.HotelId = booking.HotelId;
            existing.RoomId = booking.RoomId;
            existing.CheckInDate = booking.CheckInDate;
            existing.CheckOutDate = booking.CheckOutDate;

            var room = await _roomRepo.GetByIdAsync(booking.RoomId ?? 0);
            var season = await _seasonRepo.GetSeasonByDateRangeAsync(booking.CheckInDate, booking.CheckOutDate);
            decimal basePrice = room!.PricePerNight;
            decimal adjustedPrice = season != null ? basePrice + (basePrice * season.PriceFactor) : basePrice;

            var nights = (booking.CheckOutDate - booking.CheckInDate).Days;
            existing.TotalPrice = adjustedPrice * nights;
            

            _bookingRepo.Update(existing);
             await _bookingRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _bookingRepo.Delete(booking);
            await _bookingRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        }
}
