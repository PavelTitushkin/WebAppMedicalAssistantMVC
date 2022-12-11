using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantAPI.Model.Requests;
using WebAppMedicalAssistantAPI.Utils;

namespace WebAppMedicalAssistantAPI.Controllers
{
    /// <summary>
    /// Working with jwt
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtUtil _jwtUtil;

        public TokenController(IUserService userService, IJwtUtil jwtUtil)
        {
            _userService = userService;
            _jwtUtil = jwtUtil;
        }

        /// <summary>
        /// Login user with Jwt
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(List<DoctorVisitDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateJwtToken([FromBody] AuthorizationUserRequestModel request)
        {
            try
            {
                var isPassCorrect = await _userService.CheckUserPasswordAsync(request.Email, request.Password);

                if (!isPassCorrect)
                {
                    return StatusCode(401);
                }
                else
                {
                    var user = await _userService.GetUserByEmailAsync(request.Email);
                    if (user == null)
                    {
                        return StatusCode(404);
                    }
                    var response = await _jwtUtil.GenerateTokenAsync(user);

                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel request)
        {
            try
            {
                var user = await _userService.GetUserByRefreshTokenAsync(request.RefreshToken);

                var response = await _jwtUtil.GenerateTokenAsync(user);

                await _jwtUtil.RemoveRefreshTokenAsync(request.RefreshToken);

                return Ok(response);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Revoke token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Revoke")]
        [HttpPost]
        public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequestModel request)
        {
            try
            {
                await _jwtUtil.RemoveRefreshTokenAsync(request.RefreshToken);

                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500);
            }
        }
    }
}
