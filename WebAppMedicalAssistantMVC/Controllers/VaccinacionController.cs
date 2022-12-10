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
    public class VaccinacionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVaccinationService _vaccinationService;
        private readonly IUserService _userService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;

        public VaccinacionController(IMapper mapper, IVaccinationService vaccinationService, IUserService userService, IMedicalInstitutionService medicalInstitutionService)
        {
            _mapper = mapper;
            _vaccinationService = vaccinationService;
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
                    var dto = await _vaccinationService.GetAllVaccinationsAsync(userDto.Id);
                    if (pageIndex == 0)
                    {
                        pageIndex = 1;
                    }
                    var model = PagingList.Create(dto, 5, pageIndex);

                    return View(model);
                }
                else
                {
                    var dto = await _vaccinationService.GetPeriodVaccinationAsync(SearchDateStart, SearchDateEnd, userDto.Id);
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
        public async Task<IActionResult> Create()
        {
            try
            {
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var model = new VaccinacionModel();
                model.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution");
                model.ReturnUrl = Request.Headers["Referer"].ToString();
                if (model.ReturnUrl == "https://localhost:7068/MedicalInstitution/Create")
                {
                    model.ReturnUrl = "https://localhost:7068/Vaccinacion/Index";
                }

                return View(model);

            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(VaccinacionModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var emailUser = HttpContext.User.Identity.Name;
                    var userDto = await _userService.GetUserByEmailAsync(emailUser);
                    model.UserId = userDto.Id;

                    var dto = _mapper.Map<VaccinationDto>(model);
                    await _vaccinationService.CreateVaccinationAsync(dto);

                    return Redirect(model.ReturnUrl);
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
        public async Task<IActionResult> Edit(int id, string? applicationOfVaccine, string? nameOfVaccine, string? vacineDose, string? vacineSeria, int? medicalInstitutionDtoId)
        {
            try
            {
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();
                var model = new VaccinacionModel()
                {
                    Id = id,
                    ApplicationOfVaccine = applicationOfVaccine,
                    NameOfVaccine = nameOfVaccine,
                    VacineDose = vacineDose,
                    VacineSeria = vacineSeria,
                    MedicalInstitutionId = medicalInstitutionDtoId,
                    MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution"),
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
        public async Task<IActionResult> Edit(VaccinacionModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dto = await _vaccinationService.GetVaccinationByIdAsync(model.Id);
                    dto.ApplicationOfVaccine = model.ApplicationOfVaccine;
                    dto.NameOfVaccine = model.NameOfVaccine;
                    dto.VacineDose = model.VacineDose;
                    dto.VacineSeria = model.VacineSeria;
                    dto.MedicalInstitutionId = model.MedicalInstitutionId;

                    await _vaccinationService.UpdateVaccinationAsync(dto, dto.Id);

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id, string? applicationOfVaccine, string? nameOfVaccine)
        {
            try
            {
                var model = new VaccinacionModel()
                {
                    Id = id,
                    ApplicationOfVaccine = applicationOfVaccine,
                    NameOfVaccine = nameOfVaccine,
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
                await _vaccinationService.DeleteVaccinationAsync(id);

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
