using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
    public class VaccinacionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVaccinationService _vaccinationService;
        private readonly IUserService _userService;

        public VaccinacionController(IMapper mapper, IVaccinationService vaccinationService, IUserService userService)
        {
            _mapper = mapper;
            _vaccinationService = vaccinationService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var listVaccinations = await _vaccinationService.GetAllVaccinationsAsync(userDto.Id);

                return View(listVaccinations);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
