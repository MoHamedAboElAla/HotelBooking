using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;

        public RoomController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Room
        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms.Include(r => r.Hotel).ToListAsync();
            return View(rooms);
        }

        // GET: Room/Create
        public IActionResult Create()
        {
            ViewBag.Hotels = _context.Hotels.ToList();
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room,IFormFile Imagefile)
        {


            if (Imagefile != null && Imagefile.Length > 0)
            {
                var extension = Path.GetExtension(Imagefile.FileName).ToLower();

                if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension))
                {
                    ModelState.AddModelError("ImageUrl", "Only .jpg, .jpeg, .png files are allowed.");
                }
                else
                {
                    var fileName = Guid.NewGuid().ToString() + extension;
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "rooms");
                    var filePath = Path.Combine(folderPath, fileName);

                    Directory.CreateDirectory(folderPath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Imagefile.CopyToAsync(stream);
                    }
                    
                    room.ImageUrl = "/Uploads/rooms/" + fileName;
                }
            }
            bool exists = await _context.Rooms
                .AnyAsync(r => r.RoomNumber == room.RoomNumber && r.HotelId == room.HotelId);

            if (exists)
            {
                ModelState.AddModelError("", "This room number already exists in the selected hotel.");
                ViewBag.Hotels = _context.Hotels.ToList();
                return View(room);
            } 
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hotels = _context.Hotels.ToList();
            return View(room);
        }


        // GET: Room/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();
            ViewBag.Hotels = _context.Hotels.ToList();
            return View(room);
        }

        // POST: Room/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room, IFormFile? Imagefile)
        {
            if (id != room.Id)
                return NotFound();

            var existingRoom = await _context.Rooms.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (existingRoom == null)
                return NotFound();

            bool exists = await _context.Rooms
                .AnyAsync(r => r.RoomNumber == room.RoomNumber && r.HotelId == room.HotelId && r.Id != room.Id);
            if (exists)
            {
                ModelState.AddModelError("", "This room number already exists in the selected hotel.");
            }

            if (Imagefile != null && Imagefile.Length > 0)
            {
                var extension = Path.GetExtension(Imagefile.FileName).ToLower();
                if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension))
                {
                    ModelState.AddModelError("Imagefile", "Only .jpg, .jpeg, .png files are allowed.");
                }
                else
                {
                    var fileName = Guid.NewGuid().ToString() + extension;
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "rooms");
                    Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Imagefile.CopyToAsync(stream);
                    }

                    room.ImageUrl = "/Uploads/rooms/" + fileName;
                }
            }
            else
            {
                room.ImageUrl = existingRoom.ImageUrl;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Hotels = _context.Hotels.ToList();
                return View(room);
            }

            _context.Update(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();
            return View(room);
        }

        // POST: Room/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}