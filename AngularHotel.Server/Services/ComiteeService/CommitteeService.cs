using AngularHotel.Shared.Models.ResponseModels.Commitee;

namespace AngularHotel.Server.Services.ComiteeService
{
    public class CommitteeService : ICommitteeService
    {
        private readonly DataContext _context;

        public CommitteeService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<CommitteeResponseModel>>> GetAllComitees()
        {
            try
            {
                var committees = await _context.Users
                    .Select(c => new CommitteeResponseModel
                    {
                        Id = c.Id,
                        FirstName = c.Name,
                        LastName = c.LastName,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber
                    })
                    .ToListAsync();

                return new ServiceResponse<List<CommitteeResponseModel>>
                {
                    Data = committees,
                    Message = "Committees retrieved successfully.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<CommitteeResponseModel>>
                {
                    Success = false,
                    Message = $"Failed to retrieve committees: {ex.Message}",
                };
            }
        }
    }
}
