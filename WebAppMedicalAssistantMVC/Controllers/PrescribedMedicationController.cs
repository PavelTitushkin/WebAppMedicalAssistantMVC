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
    [Authorize]
    public class PrescribedMedicationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPrescribedMedicationService _prescribedMedicationService;
        private readonly IUserService _userService;
        private readonly IMedicineService _medicineService;

        public PrescribedMedicationController(IMapper mapper, IPrescribedMedicationService prescribedMedicationService, IUserService userService, IMedicineService medicineService)
        {
            _mapper = mapper;
            _prescribedMedicationService = prescribedMedicationService;
            _userService = userService;
            _medicineService = medicineService;
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
                    var dto = await _prescribedMedicationService.GetAllPrescribedMedicationsAsync(userDto.Id);
                    if (pageIndex == 0)
                    {
                        pageIndex = 1;
                    }
                    var model = PagingList.Create(dto, 5, pageIndex);

                    return View(model);
                }
                else
                {
                    var dto = await _prescribedMedicationService.GetPeriodPrescribedMedicationAsync(SearchDateStart, SearchDateEnd, userDto.Id);
                    var model = PagingList.Create(dto, 5, pageIndex);

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
        public async Task<IActionResult> Create(int? id)
        {
            try
            {
                var medicineDto = await _medicineService.GetAllMedicineAsync();
                var model = new PrescribedMedicationModel()
                {
                    AppointmentId = id,
                    MedicineList = new SelectList(medicineDto, "Id", "NameOfMedicine"),
                    ReturnUrl = Request.Headers["Referer"].ToString(),
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
        public async Task<IActionResult> Create(PrescribedMedicationModel model)
        {
            try
            {
                var userEmail = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(userEmail);
                model.UserId = userDto.Id;
                var dto = _mapper.Map<PrescribedMedicationDto>(model);
                await _prescribedMedicationService.CreatePrescribedMedicationAsync(dto);

                return Redirect(model.ReturnUrl);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, int medicineId, DateTime startDateOfMedication,
            DateTime? endDateOfMedication, decimal? medicinePrice, string? medicineDose)
        {
            try
            {
                var medicineDto = await _medicineService.GetAllMedicineAsync();
                var model = new PrescribedMedicationModel()
                {
                    Id = id,
                    MedicineId = medicineId,
                    StartDateOfMedication = startDateOfMedication,
                    EndDateOfMedication = endDateOfMedication,
                    MedicinePrice = medicinePrice,
                    MedicineDose = medicineDose,
                    MedicineList = new SelectList(medicineDto, "Id", "NameOfMedicine"),
                    ReturnUrl = Request.Headers["Referer"].ToString(),
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
        public async Task<IActionResult> Edit(PrescribedMedicationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dto = await _prescribedMedicationService.GetPrescribedMedicationByIdAsync(model.Id);
                    dto.StartDateOfMedication = model.StartDateOfMedication;
                    dto.EndDateOfMedication = model.EndDateOfMedication;
                    dto.MedicineId = model.MedicineId;
                    dto.MedicineDose = model.MedicineDose;
                    dto.MedicinePrice = model.MedicinePrice;
                    await _prescribedMedicationService.UpdatePrescribedMedicationAsync(dto, dto.Id);

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
        public IActionResult Delete(int id, string? nameOfMedicine)
        {
            try
            {
                var model = new PrescribedMedicationModel()
                {
                    Id = id,
                    NameOfMedicine = nameOfMedicine,
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _prescribedMedicationService.DeletePrescribedMedicationAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsPrescribedMedicationPartialView(int id)
        {
            try
            {
                var dto = await _prescribedMedicationService.GetPrescribedMedicationByIdAsync(id);

                return PartialView(dto);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }
    }
}
