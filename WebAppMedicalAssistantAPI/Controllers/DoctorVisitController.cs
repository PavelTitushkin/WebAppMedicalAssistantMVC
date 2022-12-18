using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantAPI.Model.Requests;

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
        private readonly IDoctorService _doctorService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;
        private readonly IMapper _mapper;

        public DoctorVisitController(IDoctorVisitService doctorVisitService, IUserService userService, IDoctorService doctorService, IMedicalInstitutionService medicalInstitutionService, IMapper mapper)
        {
            _doctorVisitService = doctorVisitService;
            _userService = userService;
            _doctorService = doctorService;
            _medicalInstitutionService = medicalInstitutionService;
            _mapper = mapper;
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
                if (!allDates)
                {
                    var dto = await _doctorVisitService.GetAllDoctorVisitAsync(userDto.Id);
                    if(dto == null)
                    {
                        return NotFound();
                    }

                    return Ok(dto);
                }
                else
                {
                    var dto = await _doctorVisitService.GetPeriodDoctorVisitAsync(searchDateStart, searchDateEnd, userDto.Id);
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


        /// <summary>
        /// Create doctor visit
        /// </summary>
        /// <param name="doctorVisitModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody]DoctorVisitRequestModel doctorVisitModel)
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                if (userDto!=null)
                {
                    doctorVisitModel.UserDtoId = userDto.Id;
                    var doctorVisitDto = _mapper.Map<DoctorVisitDto>(doctorVisitModel);
                    await _doctorVisitService.CreateDoctorVisitAsync(doctorVisitDto);

                    return Ok();
                }
                else
                {
                    return NotFound();
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
