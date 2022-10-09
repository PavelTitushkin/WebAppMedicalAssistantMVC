using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class FluorographyController : Controller
    {
        private readonly IFluorographyService _fluorographyService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FluorographyController(IFluorographyService fluorographyService, IMapper mapper, IUserService userService)
        {
            _fluorographyService = fluorographyService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity?.Name;
                var dtoUser = await _userService.GetUserByEmailAsync(emailUser);
                var fluorographies = await _fluorographyService.GetAllFluorographiesAsync(dtoUser.Id);

                return View(fluorographies);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
