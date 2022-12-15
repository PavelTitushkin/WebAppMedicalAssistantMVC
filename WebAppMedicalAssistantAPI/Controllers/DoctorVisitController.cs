using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistantAPI.Controllers
{
    /// <summary>
    /// Controller for work with doctor visits
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorVisitController : ControllerBase
    {
        private readonly IDoctorVisitService _doctorVisitService;
        private readonly IUserService _userService;

        public DoctorVisitController(IDoctorVisitService doctorVisitService, IUserService userService)
        {
            _doctorVisitService = doctorVisitService;
            _userService = userService;
        }

        /// <summary>
        /// Get all doctor visits
        /// </summary>
        /// <param name="searchDateStart"></param>
        /// <param name="searchDateEnd"></param>
        /// <param name="allDates"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<DoctorVisitDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Index([FromQuery] DateTime searchDateStart, DateTime searchDateEnd, bool allDates)
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var id = 3;
                if (!allDates)
                {
                    var dto = await _doctorVisitService.GetAllDoctorVisitAsync(id);
                    if(dto == null)
                    {
                        return NotFound();
                    }

                    return Ok(dto);
                }
                else
                {
                    var dto = await _doctorVisitService.GetPeriodDoctorVisitAsync(searchDateStart, searchDateEnd, id);
                    if(dto == null )
                    {
                        return NotFound();
                    }

                    return Ok(dto);
                }
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }
    }
}
