using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
    public class TransferredDiseaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITransferredDiseaseService _transferredDiseaseService;
        private readonly IUserService _userService;

        public TransferredDiseaseController(IMapper mapper, ITransferredDiseaseService transferredDiseaseService, IUserService userService)
        {
            _mapper = mapper;
            _transferredDiseaseService = transferredDiseaseService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var listTransferredDisease = await _transferredDiseaseService.GetAllTransferredDiseaseAsync(userDto.Id);

                return View(listTransferredDisease);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
