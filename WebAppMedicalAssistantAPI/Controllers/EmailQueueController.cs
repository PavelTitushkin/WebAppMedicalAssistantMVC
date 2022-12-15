using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAppMedicalAssistant_Core.Abstractions.EmailService;

namespace WebAppMedicalAssistantAPI.Controllers
{
    /// <summary>
    /// Works with email
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmailQueueController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailQueueController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> SendEmail()
        {
            try
            {
                RecurringJob.AddOrUpdate(() => _emailService.GetUsersWithNearestDoctorVisitAsync(), "0 19 * * *");

                return Ok();
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }
    }
}
