using AutoMapper;
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
    /// Controller for user operation
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IJwtUtil _jwtUtil;

        public AccountController(IUserService userService, IRoleService roleService, IMapper mapper, IJwtUtil jwtUtil)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
            _jwtUtil = jwtUtil;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(List<DoctorVisitDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestModel request)
        {
            try
            {
                var userRoleId = await _roleService.GetRoleIdByNameAsync("User");
                var userDto = _mapper.Map<UserDto>(request);
                var userIsEmailExists = await _userService.IsUserExistAsync(request.Email);
                if (userDto != null && userRoleId != null && !userIsEmailExists && request.PasswordHash == request.PasswordConfirmation)
                {
                    userDto.RoleId = userRoleId.Value;
                    var result = await _userService.RegisterUser(userDto);
                    if (result > 0)
                    {
                        var userInDbDto = await _userService.GetUserByEmailAsync(userDto.Email);
                        if (userInDbDto == null)
                        {
                            return StatusCode(404);
                        }
                        var response = await _jwtUtil.GenerateTokenAsync(userInDbDto);

                        return Ok(response);
                    }
                }

                return StatusCode(401);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }
    }
}
