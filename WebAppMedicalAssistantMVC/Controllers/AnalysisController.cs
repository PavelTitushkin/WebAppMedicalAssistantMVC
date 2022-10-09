﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAnalysisService _analysisService;
        private readonly IUserService _userService;
        public AnalysisController(IMapper mapper, IAnalysisService analysisService, IUserService userService)
        {
            _mapper = mapper;
            _analysisService = analysisService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity?.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var listAnalisis = await _analysisService.GetAllAnalysisAsync(userDto.Id);

                return View(listAnalisis);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
