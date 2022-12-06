using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
    public class FluorographyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFluorographyService _fluorographyService;
        private readonly IUserService _userService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;
        public FluorographyController(IFluorographyService fluorographyService, IMapper mapper, IUserService userService, IMedicalInstitutionService medicalInstitutionService)
        {
            _fluorographyService = fluorographyService;
            _mapper = mapper;
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
                    var dto = await _fluorographyService.GetAllFluorographiesAsync(userDto.Id);
                    if (pageIndex == 0)
                    {
                        pageIndex = 1;
                    }
                    var model = PagingList.Create(dto, 5, pageIndex);

                    return View(model);
                }
                else
                {
                    var dto = await _fluorographyService.GetPeriodfluorographyAsync(SearchDateStart, SearchDateEnd, userDto.Id);
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
        public async Task<IActionResult> Create()
        {
            try
            {
                var medicalInstitutions = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                var fluorographyModel = new FluorographyModel();
                fluorographyModel.MedicalInstitutionList = new SelectList(medicalInstitutions, "Id", "NameMedicalInstitution");

                return View(fluorographyModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(FluorographyModel fluorographyModel)
        {
            try
            {
                var userEmail = HttpContext.User.Identity.Name;
                var dtoUser = await _userService.GetUserByEmailAsync(userEmail);
                fluorographyModel.UserId = dtoUser.Id;
                var fluorographyDto = _mapper.Map<FluorographyDto>(fluorographyModel);
                await _fluorographyService.CreatefluorographyAsync(fluorographyDto);

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, DateTime dataOfExamination, DateTime endDateOfSurvey, int medicalInstitutionDtoId, string numberFluorography)
        {
            try
            {
                var medicalInstitutionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();
                var model = new FluorographyModel()
                {
                    Id = id,
                    DataOfExamination = dataOfExamination,
                    EndDateOfSurvey = endDateOfSurvey,
                    MedicalInstitutionId = medicalInstitutionDtoId,
                    MedicalInstitutionList = new SelectList(medicalInstitutionsDto, "Id", "NameMedicalInstitution"),
                    NumberFluorography = numberFluorography,
                };

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FluorographyModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dto = await _fluorographyService.GetFluorographyByIdAsync(model.Id);
                    dto.DataOfExamination = model.DataOfExamination;
                    dto.EndDateOfSurvey = model.EndDateOfSurvey;
                    dto.NumberFluorography = model.NumberFluorography;
                    dto.MedicalInstitutionId = model.MedicalInstitutionId;

                    await _fluorographyService.UpdateFluorographyAsync(dto, dto.Id);

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Delete(int id, DateTime dataOfExamination,string numberFluorography)
        {
            try
            {
                var model = new FluorographyModel()
                {
                    Id = id,
                    DataOfExamination = dataOfExamination,
                    NumberFluorography = numberFluorography
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
                await _fluorographyService.DeleteFluorographyAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
