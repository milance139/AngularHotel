using AngularHotel.Shared.Models.RequestModels.Reservation;
using AngularHotel.Shared.Models.ResponseModels.Reservation;

namespace AngularHotel.Server.Services.ReservationService
{
    public interface IReservationService
    {
        Task<ServiceResponse<Reservation>> CreateReservation(CreateReservationRequestModel requestModel);
        Task<ServiceResponse<List<ReservationResponseModel>>> GetAllReservations();
        Task<ServiceResponse<bool>> CancelReservation(int reservationId);
        Task<ServiceResponse<bool>> DeleteReservation(int reservationId);
        Task<ServiceResponse<List<ReservationArchiveResponseModel>>> GetAllArchivedReservations();
        Task<ServiceResponse<Reservation>> UpdateReservation(CreateReservationRequestModel requestModel);
    }
}
