using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
    //[Authorize]
    public class AnalysisController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAnalysisService _analysisService;
        private readonly IUserService _userService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;

        public AnalysisController(IMapper mapper, IAnalysisService analysisService, IUserService userService, IMedicalInstitutionService medicalInstitutionService)
        {
            _mapper = mapper;
            _analysisService = analysisService;
            _userService = userService;
            _medicalInstitutionService = medicalInstitutionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex, DateTime SearchDateStart, DateTime SearchDateEnd, bool AllDates)
        {
            try
            {
                var emailUser = HttpContext.User.Identity?.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);

                if (!AllDates)
                {
                    var listAnalisis = await _analysisService.GetAllAnalysisAsync(userDto.Id);
                    if (pageIndex == 0)
                    {
                        pageIndex = 1;
                    }
                    var model =  PagingList.Create(listAnalisis, 5, pageIndex);

                    return View(model);
                }
                else
                {
                    var listAnalisis = await _analysisService.GetPeriodAnalysisAsync(SearchDateStart, SearchDateEnd, userDto.Id);
                    var model = PagingList.Create(listAnalisis, 5, pageIndex);

                    return View(model);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            try
            {
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var analysisModel = new AnalysisModel();
                analysisModel.AppointmentId = id;
                analysisModel.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution");
                analysisModel.ReturnUrl = Request.Headers["Referer"].ToString();
                if (analysisModel.ReturnUrl == "https://localhost:7068/MedicalInstitution/Create")
                {
                    analysisModel.ReturnUrl = "https://localhost:7068/Analysis/Index";
                }

                return View(analysisModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnalysisModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    var emailUser = HttpContext.User.Identity.Name;
                    var userDto = await _userService.GetUserByEmailAsync(emailUser);
                    model.UserId = userDto.Id;
                    var dto = _mapper.Map<AnalysisDto>(model);
                    var idAnalysis = await _analysisService.CreateAnalysisAsync(dto);
                    var returnUrl = model.ReturnUrl;

                    if (model.ScanOfAnalysisOne != null || model.ScanOfAnalysisTwo != null || model.ScanOfAnalysisThree != null || model.ScanOfAnalysisFour != null || model.ScanOfAnalysisFive != null)
                    {
                        var scansFormFile = new List<IFormFile?>();
                        if(model.ScanOfAnalysisOne != null)
                            scansFormFile.Add(model?.ScanOfAnalysisOne);
                        if (model.ScanOfAnalysisTwo != null)
                            scansFormFile.Add(model?.ScanOfAnalysisTwo);
                        if (model.ScanOfAnalysisThree != null)
                            scansFormFile.Add(model?.ScanOfAnalysisThree);
                        if (model.ScanOfAnalysisFour != null)
                            scansFormFile.Add(model?.ScanOfAnalysisFour);
                        if (model.ScanOfAnalysisFive != null)
                            scansFormFile.Add(model?.ScanOfAnalysisFive);

                        foreach (var item in scansFormFile)
                        {
                            byte[] imageData = null;
                            var dtoScan = new ScanOfAnalysisDocumentDto();
                            using (BinaryReader binaryReader = new BinaryReader(item.OpenReadStream()))
                            {
                                imageData = binaryReader.ReadBytes((int)item.Length);
                            }
                            dtoScan.ScanOfAnalysis = imageData;
                            dtoScan.AnalysisId = idAnalysis;

                            await _analysisService.CreateScanOfDocumentsAnalysisAsync(dtoScan);
                        }

                        return Redirect(returnUrl);
                    }

                    return Redirect(returnUrl);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsAnalysisPartialView(int id)
        {
            try
            {
                var dto = await _analysisService.GetAnalysisByIdAsync(id);

                return PartialView(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
