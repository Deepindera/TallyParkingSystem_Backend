using Microsoft.AspNetCore.Mvc;
using TallyParkingSystem.Model;
using TallyParkingSystem.Services;

namespace TallyParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingRateController : ControllerBase
    {
        [HttpPost]
        public ActionResult<IEntryResponse> Post(EntryRequest request)
        {
            
            var entryResponse = ParkingRateService.CalculateParkingFee(request);
            return Ok(entryResponse);
        }
    }
}
