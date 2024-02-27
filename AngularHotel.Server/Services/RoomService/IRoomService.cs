namespace AngularHotel.Server.Services.RoomService
{
    public interface IRoomService
    {
        Task<ServiceResponse<Room>> CreateRoom(Room room);
        Task<ServiceResponse<Room>> UpdateRoom(Room room);
        Task<ServiceResponse<List<Room>>> GetAllRooms();
        Task<ServiceResponse<bool>> DeleteRoom(int roomId);
    }
}
