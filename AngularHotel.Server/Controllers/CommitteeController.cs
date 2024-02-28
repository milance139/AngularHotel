using AngularHotel.Shared.Models.ResponseModels.Commitee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AngularHotel.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitteeController : ControllerBase
    {
        private readonly ICommitteeService _comiteeService;

        public CommitteeController(ICommitteeService comiteeService)
        {
            _comiteeService = comiteeService;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        [Route("get-all-comitees")]
        public async Task<ActionResult<ServiceResponse<List<CommitteeResponseModel>>>> GetAllCommittees()
        {
            var result = await _comiteeService.GetAllComitees();

            return Ok(result);
        }
    }
}
