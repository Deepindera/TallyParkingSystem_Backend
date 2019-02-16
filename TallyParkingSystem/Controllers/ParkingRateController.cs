using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TallyParkingSystem.Model;
using TallyParkingSystem.Services;

namespace TallyParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigin")]
    [ApiController]
    public class ParkingRateController : ControllerBase
    {
        [HttpPost]
        [EnableCors("AllowAllOrigin")]
        public ActionResult<IEntryResponse> Post(EntryRequest request)
        {
            var entryResponse = ParkingRateService.CalculateParkingFee(request);
            return Ok(entryResponse);
        }
    }
}
