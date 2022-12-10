using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using Serilog;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
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
                    var model = PagingList.Create(listAnalisis, 5, pageIndex);

                    return View(model);
                }
                else
                {
                    var listAnalisis = await _analysisService.GetPeriodAnalysisAsync(SearchDateStart, SearchDateEnd, userDto.Id);
                    var model = PagingList.Create(listAnalisis, 5, pageIndex);

                    return View(model);
                }
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string nameOfAnalysis, DateTime dateOfAnalysis, int medicalInstitutionId)
        {
            try
            {
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var analysisModel = new AnalysisModel();
                analysisModel.Id = id;
                analysisModel.NameOfAnalysis = nameOfAnalysis;
                analysisModel.DateOfAnalysis = dateOfAnalysis;
                analysisModel.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution");
                analysisModel.MedicalInstitutionId = medicalInstitutionId;
                analysisModel.ReturnUrl = Request.Headers["Referer"].ToString();
                if (analysisModel.ReturnUrl == "https://localhost:7068/MedicalInstitution/Create")
                {
                    analysisModel.ReturnUrl = "https://localhost:7068/Analysis/Index";
                }

                return View(analysisModel);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AnalysisModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dto = await _analysisService.GetAnalysisByIdAsync(model.Id);
                    dto.DateOfAnalysis = model.DateOfAnalysis;
                    dto.MedicalInstitutionId = model.MedicalInstitutionId;
                    dto.NameOfAnalysis = model.NameOfAnalysis;
                    await _analysisService.UpdateAnalysisAsync(dto, dto.Id);

                    return Redirect(model.ReturnUrl);

                    //string errorMessages = "";
                    //// проходим по всем элементам в ModelState
                    //foreach (var item in ModelState)
                    //{
                    //    // если для определенного элемента имеются ошибки
                    //    if (item.Value.ValidationState == ModelValidationState.Invalid)
                    //    {
                    //        errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
                    //        // пробегаемся по всем ошибкам
                    //        foreach (var error in item.Value.Errors)
                    //        {
                    //            errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                    //        }
                    //    }
                    //}

                    ////return errorMessages;
                    //return View(errorMessages);

                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult AddScanOfAnalysis(int id, string nameOfAnalysis, DateTime dateOfAnalysis)
        {
            try
            {
                var model = new AnalysisModel()
                {
                    Id = id,
                    NameOfAnalysis = nameOfAnalysis,
                    DateOfAnalysis = dateOfAnalysis,
                };

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddScanOfAnalysis(AnalysisModel model)
        {
            try
            {
                if (model.ScanOfAnalysisOne != null || model.ScanOfAnalysisTwo != null || model.ScanOfAnalysisThree != null || model.ScanOfAnalysisFour != null || model.ScanOfAnalysisFive != null)
                {
                    var scansFormFile = new List<IFormFile?>();
                    if (model.ScanOfAnalysisOne != null)
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
                        dtoScan.AnalysisId = model.Id;

                        await _analysisService.CreateScanOfDocumentsAnalysisAsync(dtoScan);
                    }

                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditScanOfAnalysis(int id)
        {
            try
            {
                var dto = await _analysisService.GetScanOfAnalysisByIdAsync(id);
                var model = new ScanOfAnalysisDocumentModel()
                {
                    Id = id,
                    ScanOfAnalysisByte = dto.ScanOfAnalysis
                };

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditScanOfAnalysis(ScanOfAnalysisDocumentModel model)
        {
            try
            {
                if (model.ScanOfAnalysis != null)
                {
                    byte[] imageData = null;
                    var dto = new ScanOfAnalysisDocumentDto()
                    {
                        Id = model.Id,
                    };
                    using (BinaryReader binaryReader = new BinaryReader(model.ScanOfAnalysis.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.ScanOfAnalysis.Length);
                    }
                    dto.ScanOfAnalysis = imageData;

                    await _analysisService.UpdateScanOfDocumentsAnalysisAsync(dto);

                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
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
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnalysisModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var emailUser = HttpContext.User.Identity.Name;
                    var userDto = await _userService.GetUserByEmailAsync(emailUser);
                    model.UserId = userDto.Id;
                    var dto = _mapper.Map<AnalysisDto>(model);
                    var idAnalysis = await _analysisService.CreateAnalysisAsync(dto);
                    var returnUrl = model.ReturnUrl;

                    return Redirect(returnUrl);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
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
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult DeleteScanOfAnalysis(int id, string nameOfAnalysis, DateTime dateOfAnalysis)
        {
            try
            {
                var model = new ScanOfAnalysisDocumentModel()
                {
                    Id = id,
                    NameOfAnalysis = nameOfAnalysis,
                    DateOfAnalysis = dateOfAnalysis
                };

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteScanOfAnalysis(int id)
        {
            try
            {
                await _analysisService.DeleteScanOfAnalysisAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id, string nameOfAnalysis, DateTime dateOfAnalysis)
        {
            try
            {
                var model = new AnalysisModel();
                model.DateOfAnalysis = dateOfAnalysis;
                model.NameOfAnalysis = nameOfAnalysis;
                model.Id = id;

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _analysisService.DeleteAnalysisAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

    }
}
