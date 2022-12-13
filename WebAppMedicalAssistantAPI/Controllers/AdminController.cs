using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Bussines.EmailService;
using MimeKit;
using WebAppMedicalAssistant_Core.Abstractions.EmailService;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantAPI.Controllers
{
    /// <summary>
    /// Works with admin
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IAdminService _adminService;

        public AdminController(IEmailSender emailSender, IAdminService adminService)
        {
            _emailSender = emailSender;
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> SendEmail()
        {
            var messages = await _adminService.GetUsersWithNearestDoctorVisitAsync();
            await _emailSender.SendRangeEmailAsync(messages);

            return Ok();
        }
    }
}
