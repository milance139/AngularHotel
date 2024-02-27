using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularHotel.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        [Route("get-all-rooms")]
        public async Task<ActionResult<ServiceResponse<List<Room>>>> GetAllRooms()
        {
            var result = await _roomService.GetAllRooms();

            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Room>>> CreateRoom(Room room)
        {
            var result = await _roomService.CreateRoom(room);

            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Room>>> UpdateRoom(Room room)
        {
            var result = await _roomService.UpdateRoom(room);

            return Ok(result);
        }

        [HttpDelete("{roomId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteRoom(int roomId)
        {
            var result = await _roomService.DeleteRoom(roomId);

            return Ok(result);
        }
    }
}
