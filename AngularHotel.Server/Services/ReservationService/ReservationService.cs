using AngularHotel.Shared.Models;
using AngularHotel.Shared.Models.Entities;
using AngularHotel.Shared.Models.RequestModels.Reservation;
using AngularHotel.Shared.Models.ResponseModels.CurrencyResponse;
using AngularHotel.Shared.Models.ResponseModels.Reservation;
using System.Collections.Generic;


namespace AngularHotel.Server.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext _context;
        private readonly ICurrencyService _currencyService;

        public ReservationService(DataContext context, ICurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        public async Task<ServiceResponse<bool>> CancelReservation(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);

            if (reservation == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Reservation not found."
                };
            }

            reservation.IsCancelled = true;
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Reservation successfully cancelled.",
                Data = true
            };
        }

        public async Task<ServiceResponse<Reservation>> CreateReservation(CreateReservationRequestModel requestModel)
        {
            var roomsExists = await _context.Rooms.AnyAsync(r => requestModel.ReservedRoomIds.Contains(r.Id));
            if (!roomsExists)
            {
                return new ServiceResponse<Reservation>
                {
                    Message = "One or more sepcified rooms do not exists.",
                    Success = false
                };
            }

            var conflictingRooms = await _context.ReservedRooms
                                                 .Where(rr => requestModel.ReservedRoomIds.Contains(rr.RoomId) &&
                                                               !rr.Reservation.IsCancelled &&
                                                               (rr.Reservation.From <= requestModel.To && rr.Reservation.To >= requestModel.From))
                                                 .Select(rr => rr.RoomId)
                                                 .ToListAsync();

            if (conflictingRooms.Any())
            {
                // At least one room is not available for the specified dates
                var unavailableRoomId = conflictingRooms.First();
                return new ServiceResponse<Reservation>
                {
                    Message = $"Room with ID {unavailableRoomId} is not available for the specified dates.",
                    Success = false
                };
            }

            var reservation = new Reservation
            {
                From = requestModel.From,
                To = requestModel.To,
                Discount = requestModel.Discount ?? 0,
                OriginalPrice = requestModel.OriginalPrice,
                TotalPrice = GetTotalPrice(requestModel.Discount, requestModel.OriginalPrice),
                CurrencyId = requestModel.CurrencyId,
                ReservationCommitteeId = requestModel.ReservationCommitteeId,
                ReservedRooms = requestModel.ReservedRoomIds.Select(roomId => new ReservedRoom { RoomId = roomId }).ToList()
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Reservation>
            {
                Data = reservation,
                Message = "Reservation created succesfully.",
                Success = true
            };
        }

        public async Task<ServiceResponse<bool>> DeleteReservation(int reservationId)
        {
            var dbReservation = await _context.Reservations.FindAsync(reservationId);
            if (dbReservation == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Reservation not found",
                    Success = false
                };
            }

            _context.Reservations.Remove(dbReservation);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true, Message = "Reservation deleted successfully", Success = true };
        }

        public async Task<ServiceResponse<List<ReservationArchiveResponseModel>>> GetAllArchivedReservations()
        {
            try
            {
                var response = await _context.Reservations
                                             .Select(r => new ReservationArchiveResponseModel
                                             {
                                                 From = r.From,
                                                 To = r.To,
                                                 ReservationCommitteeFullName = $"{r.ReservationCommittee.Name} {r.ReservationCommittee.LastName}",
                                                 OriginalPrice = r.OriginalPrice,
                                                 Discount = r.Discount,
                                                 TotalPrice = r.TotalPrice,
                                                 Currency = r.Currency,
                                                 TotalPriceInBAMAndEur = _currencyService.GetPriceInBAMAndEur(r.TotalPrice, r.Currency).Data,
                                                 IsCancelled = r.IsCancelled,
                                                 ReservedRooms = r.ReservedRooms.Select(rr => rr.Room.Name).ToList()
                                             })
                                             .ToListAsync();


                return new ServiceResponse<List<ReservationArchiveResponseModel>>
                {
                    Success = true,
                    Message = "Archived reservations retrieved successfully.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<ReservationArchiveResponseModel>>
                {
                    Success = false,
                    Message = $"Failed to retrieve archived reservations: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ServiceResponse<List<ReservationResponseModel>>> GetAllReservations()
        {
            var serviceResponse = new ServiceResponse<List<ReservationResponseModel>>();

            try
            {
                var reservations = await _context.Reservations
                                                  .Select(r => new ReservationResponseModel
                                                  {
                                                      Id = r.Id,
                                                      From = r.From,
                                                      To = r.To,
                                                      ReservationCommitteeFullName = $"{r.ReservationCommittee.Name} {r.ReservationCommittee.LastName}",
                                                      OriginalPrice = r.OriginalPrice,
                                                      Discount = r.Discount,
                                                      TotalPrice = r.TotalPrice,
                                                      Currency = r.Currency,
                                                      IsCancelled = r.IsCancelled,
                                                      ReservedRooms = r.ReservedRooms.Select(rr => rr.Room.Name).ToList()
                                                  })
                                                  .ToListAsync();

                serviceResponse.Data = reservations;
                serviceResponse.Message = "All reservations retrieved successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error occurred while retrieving reservations: {ex.Message}";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Reservation>> UpdateReservation(CreateReservationRequestModel requestModel)
        {
            try
            {
                // Check if the reservation exists
                var existingReservation = await _context.Reservations
                    .Include(r => r.ReservedRooms)
                    .FirstOrDefaultAsync(r => r.Id == requestModel.Id);

                if (existingReservation == null)
                {
                    return new ServiceResponse<Reservation>
                    {
                        Success = false,
                        Message = "Reservation not found.",
                        Data = null
                    };
                }

                // Update reservation properties
                existingReservation.From = requestModel.From;
                existingReservation.To = requestModel.To;
                existingReservation.ReservationCommitteeId = requestModel.ReservationCommitteeId;
                existingReservation.OriginalPrice = requestModel.OriginalPrice;
                existingReservation.Discount = requestModel.Discount ?? 0;
                existingReservation.CurrencyId = requestModel.CurrencyId;

                // Update reserved rooms
                existingReservation.ReservedRooms.Clear();
                foreach (var roomId in requestModel.ReservedRoomIds)
                {
                    var room = await _context.Rooms.FindAsync(roomId);
                    if (room != null)
                    {
                        existingReservation.ReservedRooms.Add(new ReservedRoom
                        {
                            RoomId = room.Id
                        });
                    }
                }

                // Save changes
                _context.Update(existingReservation);
                await _context.SaveChangesAsync();

                return new ServiceResponse<Reservation>
                {
                    Data = existingReservation,
                    Message = "Reservation updated successfully.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Reservation>
                {
                    Message = $"Failed to update reservation: {ex.Message}",
                    Success = false
                };
            }
        }

        private decimal GetTotalPrice(int? discount, decimal originalPrice)
        {
            if (discount.HasValue && discount.Value > 0)
            {
                decimal discountPercentage = (decimal)discount.Value / 100;
                decimal discountAmount = originalPrice * discountPercentage;
                return originalPrice - discountAmount;
            }
            else
            {
                return originalPrice;
            }
        }
    }
}
