using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class DoctorVisitController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDoctorVisitService _doctorVisitService;
        private readonly IUserService _userService;

        public DoctorVisitController(IMapper mapper, IDoctorVisitService doctorVisitService, IUserService userService)
        {
            _mapper = mapper;
            _doctorVisitService = doctorVisitService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var listDoctorVisits = await _doctorVisitService.GetAllDoctorVisitAsync(userDto.Id);
                
                return View(listDoctorVisits);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
