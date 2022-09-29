using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Core.Abstractions;

namespace WebAppMedicalAssistantMVC.Controllers
{
    public class TransferredDiseaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITransferredDiseaseService _transferredDiseaseService;

        public TransferredDiseaseController(IMapper mapper, ITransferredDiseaseService transferredDiseaseService)
        {
            _mapper = mapper;
            _transferredDiseaseService = transferredDiseaseService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var listTransferredDisease = await _transferredDiseaseService.GetAllTransferredDiseaseAsync();
                if(listTransferredDisease.Any())
                {
                    return View(listTransferredDisease);
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
