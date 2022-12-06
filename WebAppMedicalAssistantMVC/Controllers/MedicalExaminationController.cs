using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
    public class MedicalExaminationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMedicalExaminationService _medicalExaminationService;
        private readonly IUserService _userService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;

        public MedicalExaminationController(IMapper mapper, IMedicalExaminationService medicalExaminationService, IUserService userService, IMedicalInstitutionService medicalInstitutionService)
        {
            _mapper = mapper;
            _medicalExaminationService = medicalExaminationService;
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
                    var dto = await _medicalExaminationService.GetAllMedicalExaminationAsync(userDto.Id);
                    if (pageIndex == 0)
                    {
                        pageIndex = 1;
                    }
                    var model = PagingList.Create(dto, 5, pageIndex);

                    return View(model);
                }
                else
                {
                    var dto = await _medicalExaminationService.GetPeriodMedicalExaminationAsync(SearchDateStart, SearchDateEnd, userDto.Id);
                    var model = PagingList.Create(dto, 5, pageIndex);

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
                var model = new MedicalExaminationModel();
                model.AppointmentId = id;
                model.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution");
                model.ReturnUrl = Request.Headers["Referer"].ToString();
                if (model.ReturnUrl == "https://localhost:7068/MedicalInstitution/Create")
                {
                    model.ReturnUrl = "https://localhost:7068/MedicalExamination/Index";
                }

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicalExaminationModel model)
        {
            try
            {
                var userEmail = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(userEmail);
                model.UserId = userDto.Id;
                var dto = _mapper.Map<MedicalExaminationDto>(model);
                await _medicalExaminationService.CreateMedicalExaminationAsync(dto);

                return Redirect(model.ReturnUrl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsMedicalExaminationPartialView(int id)
        {
            try
            {
                var dto = await _medicalExaminationService.GetMedicalExaminationByIdAsync(id);

                return PartialView(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult AddScanOfMedicalExamination(int id, string nameOfMedicalExamination, DateTime dateOfMedicalExamination)
        {
            try
            {
                var model = new MedicalExaminationModel()
                {
                    Id = id,
                    NameOfMedicalExamination = nameOfMedicalExamination,
                    DateOfMedicalExamination = dateOfMedicalExamination,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddScanOfMedicalExamination(MedicalExaminationModel model)
        {
            try
            {
                if (model.ScanOfMedicalExaminationOne != null || model.ScanOfMedicalExaminationTwo != null || model.ScanOfMedicalExaminationThree != null || model.ScanOfMedicalExaminationFour != null || model.ScanOfMedicalExaminationFive != null)
                {
                    var scansFormFile = new List<IFormFile?>();
                    if (model.ScanOfMedicalExaminationOne != null)
                        scansFormFile.Add(model?.ScanOfMedicalExaminationOne);
                    if (model.ScanOfMedicalExaminationTwo != null)
                        scansFormFile.Add(model?.ScanOfMedicalExaminationTwo);
                    if (model.ScanOfMedicalExaminationThree != null)
                        scansFormFile.Add(model?.ScanOfMedicalExaminationThree);
                    if (model.ScanOfMedicalExaminationFour != null)
                        scansFormFile.Add(model?.ScanOfMedicalExaminationFour);
                    if (model.ScanOfMedicalExaminationFive != null)
                        scansFormFile.Add(model?.ScanOfMedicalExaminationFive);

                    foreach (var item in scansFormFile)
                    {
                        byte[] imageData = null;
                        var dtoScan = new ScanOfMedicalExaminationDto();
                        using (BinaryReader binaryReader = new BinaryReader(item.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)item.Length);
                        }
                        dtoScan.ScanData = imageData;
                        dtoScan.MedicalExaminationId = model.Id;

                        await _medicalExaminationService.CreateScanOfMedicalExaminationAsync(dtoScan);
                    }

                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditScanOfMedicalExamination(int id)
        {
            try
            {
                var dto = await _medicalExaminationService.GetScanOfMedicalExaminationByIdAsync(id);
                var model = new ScanOfMedicalExaminationModel()
                {
                    Id = id,
                    ScanOfMedicalExaminationByte = dto.ScanData
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditScanOfMedicalExamination(ScanOfMedicalExaminationModel model)
        {
            try
            {
                if (model.ScanOfMedicalExamination != null)
                {
                    byte[] imageData = null;
                    var dto = new ScanOfMedicalExaminationDto()
                    {
                        Id = model.Id,
                    };
                    using (BinaryReader binaryReader = new BinaryReader(model.ScanOfMedicalExamination.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.ScanOfMedicalExamination.Length);
                    }
                    dto.ScanData = imageData;

                    await _medicalExaminationService.UpdateScanOfMedicalExaminationAsync(dto);

                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id,string nameOfMedicalExamination, DateTime dateOfMedicalExamination,decimal priceOfMedicalExamination, int medicalInstitutionId)
        {
            try
            {
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var model = new MedicalExaminationModel()
                {
                    Id = id,
                    NameOfMedicalExamination = nameOfMedicalExamination,
                    DateOfMedicalExamination = dateOfMedicalExamination,
                    PriceOfMedicalExamination = priceOfMedicalExamination,
                    MedicalInstitutionId = medicalInstitutionId,
                    MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution"),
                    ReturnUrl = Request.Headers["Referer"].ToString(),
                };
                if (model.ReturnUrl == "https://localhost:7068/MedicalInstitution/Create")
                {
                    model.ReturnUrl = "https://localhost:7068/Analysis/Index";
                }

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicalExaminationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dto = await _medicalExaminationService.GetMedicalExaminationByIdAsync(model.Id);
                    dto.NameOfMedicalExamination = model.NameOfMedicalExamination;
                    dto.DateOfMedicalExamination = model.DateOfMedicalExamination;
                    dto.MedicalInstitutionId = model.MedicalInstitutionId;
                    dto.PriceOfMedicalExamination = model.PriceOfMedicalExamination;
                    await _medicalExaminationService.UpdateMedicalExaminationAsync(dto, dto.Id);

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
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult Delete(int id, string nameOfMedicalExamination, DateTime dateOfMedicalExamination)
        {
            try
            {
                var model = new MedicalExaminationModel()
                {
                    Id = id,
                    NameOfMedicalExamination = nameOfMedicalExamination,
                    DateOfMedicalExamination = dateOfMedicalExamination,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _medicalExaminationService.DeleteMedicalExaminationAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
