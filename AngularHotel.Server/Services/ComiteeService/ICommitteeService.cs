using AngularHotel.Shared.Models.ResponseModels.Commitee;

namespace AngularHotel.Server.Services.ComiteeService
{
    public interface ICommitteeService
    {
        Task<ServiceResponse<List<CommitteeResponseModel>>> GetAllComitees();
    }
}
