using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAnalysisService _analysisService;

        public AnalysisController(IMapper mapper, IAnalysisService analysisService)
        {
            _mapper = mapper;
            _analysisService = analysisService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var id = 2;
                var listAnalisis = await _analysisService.GetAllAnalysisAsync(id);
                if(listAnalisis.Any())
                {
                    return View(listAnalisis);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
