using AngularHotel.Shared.Models.RequestModels.Reservation;
using AngularHotel.Shared.Models.ResponseModels.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularHotel.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Reservation>>> CreateReservation(CreateReservationRequestModel reservationRequestModel)
        {
            var result = await _reservationService.CreateReservation(reservationRequestModel);

            return Ok(result);
        }

        [HttpGet]
        [Route("get-all-reservations")]
        public async Task<ActionResult<ServiceResponse<List<ReservationResponseModel>>>> GetAllReservations()
        {
            var result = await _reservationService.GetAllReservations();

            return Ok(result);
        }

        [HttpGet]
        [Route("get-all-archived-reservations")]
        public async Task<ActionResult<ServiceResponse<List<ReservationResponseModel>>>> GetAllArchivedReservations()
        {
            var result = await _reservationService.GetAllArchivedReservations();

            return Ok(result);
        }


        [HttpPut, Route("cancel-reservation/{reservationId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateProduct(int reservationId)
        {
            var result = await _reservationService.CancelReservation(reservationId);

            return Ok(result);
        }

        [HttpPut, Route("update-reservation"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Reservation>>> UpdateReservation(CreateReservationRequestModel requestModel)
        {
            var result = await _reservationService.UpdateReservation(requestModel);

            return Ok(result);
        }

        [HttpDelete("{reservationId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteReservation(int reservationId)
        {
            var result = await _reservationService.DeleteReservation(reservationId);

            return Ok(result);
        }

    }
}
