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
    public class PhysicalTherapyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPhysicalTherapyService _physicalTherapyService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;
        private readonly IUserService _userService;

        public PhysicalTherapyController(IMapper mapper, IPhysicalTherapyService physicalTherapyService, IUserService userService, IMedicalInstitutionService medicalInstitutionService)
        {
            _mapper = mapper;
            _physicalTherapyService = physicalTherapyService;
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
                    var dto = await _physicalTherapyService.GetAllPhysicalTherapyAsync(userDto.Id);
                    if (pageIndex == 0)
                    {
                        pageIndex = 1;
                    }
                    var model = PagingList.Create(dto, 5, pageIndex);

                    return View(model);
                }
                else
                {
                    var dto = await _physicalTherapyService.GetPeriodPhysicalTherapyAsync(SearchDateStart, SearchDateEnd, userDto.Id);
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
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var physicalTherapyModel = new PhysicalTherapyModel();
                physicalTherapyModel.AppointmentId = id;
                physicalTherapyModel.MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution");
                physicalTherapyModel.ReturnUrl = Request.Headers["Referer"].ToString();

                return View(physicalTherapyModel);

            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PhysicalTherapyModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var emailUser = HttpContext.User.Identity.Name;
                    var userDto = await _userService.GetUserByEmailAsync(emailUser);
                    model.UserId = userDto.Id;

                    var dto = _mapper.Map<PhysicalTherapyDto>(model);
                    await _physicalTherapyService.CreatePhysicalTherapyAsync(dto);

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
        public async Task<IActionResult> Edit(int id, string nameOfPhysicalTherapy, DateTime datePhysicalTherapy, int medicalInstitutionDtoId)
        {
            try
            {
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();
                var model = new PhysicalTherapyModel()
                {
                    Id = id,
                    DatePhysicalTherapy = datePhysicalTherapy,
                    NameOfPhysicalTherapy = nameOfPhysicalTherapy,
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
        public async Task<IActionResult> Edit(PhysicalTherapyModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dto = await _physicalTherapyService.GetPhysicalTherapyByIdAsync(model.Id);
                    dto.DatePhysicalTherapy = model.DatePhysicalTherapy;
                    dto.MedicalInstitutionId = model.MedicalInstitutionId;

                    await _physicalTherapyService.UpdatePhysicalTherapyAsync(dto, dto.Id);

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
        public IActionResult Delete(int id, string nameOfPhysicalTherapy, DateTime datePhysicalTherapy)
        {
            try
            {
                var model = new PhysicalTherapyModel()
                {
                    Id = id,
                    NameOfPhysicalTherapy = nameOfPhysicalTherapy,
                    DatePhysicalTherapy = datePhysicalTherapy,
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
                await _physicalTherapyService.DeletePhysicalTherapyAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsPhysicalTherapyPartialView(int id)
        {
            try
            {
                var dto = await _physicalTherapyService.GetPhysicalTherapyByIdAsync(id);

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
