
using AngularHotel.Shared.Models.Entities;

namespace AngularHotel.Server.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoomService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Room>> CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Room> { Data = room };
        }

        public async Task<ServiceResponse<bool>> DeleteRoom(int roomId)
        {
            var dbRoom = await _context.Rooms.FindAsync(roomId);
            if (dbRoom == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Room not found",
                    Success = false
                };
            }

            dbRoom.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Room>>> GetAllRooms()
        {
            var rooms = new List<Room>();

            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                rooms = await _context.Rooms.ToListAsync();
            }
            else
                rooms = await _context.Rooms
                                        .Where(r => !r.IsDeleted)
                                        .ToListAsync();
            return new ServiceResponse<List<Room>> { Data = rooms };
        }

        public async Task<ServiceResponse<Room>> UpdateRoom(Room room)
        {
            var dbRoom = await _context.Rooms.FindAsync(room.Id);

            if (dbRoom == null)
            {
                return new ServiceResponse<Room>
                {
                    Message = "Room not found",
                    Success = false
                };
            }

            dbRoom.Name = room.Name;
            dbRoom.Description = room.Description;
            dbRoom.Capacity = room.Capacity;
            dbRoom.Code = room.Code;
            dbRoom.RoomType = room.RoomType;

            _context.SaveChangesAsync();

            return new ServiceResponse<Room> { Data = room };
        }
    }
}
