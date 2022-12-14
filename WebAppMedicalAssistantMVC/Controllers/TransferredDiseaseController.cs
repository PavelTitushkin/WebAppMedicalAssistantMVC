using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using Serilog;
using WebAppMedicalAssistant_Core;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "user")]
    public class TransferredDiseaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITransferredDiseaseService _transferredDiseaseService;
        private readonly IUserService _userService;
        private readonly IDoctorVisitService _doctorVisitService;
        public TransferredDiseaseController(IMapper mapper, ITransferredDiseaseService transferredDiseaseService, IUserService userService, IDoctorVisitService doctorVisitService)
        {
            _mapper = mapper;
            _transferredDiseaseService = transferredDiseaseService;
            _userService = userService;
            _doctorVisitService = doctorVisitService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex, DateTime SearchDateStart, DateTime SearchDateEnd, bool AllDates)
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                if (!AllDates)
                {
                    var listTransferredDisease = await _transferredDiseaseService.GetAllTransferredDiseaseAsync(userDto.Id);
                    if (pageIndex == 0)
                    {
                        pageIndex = 1;
                    }
                    var model = PagingList.Create(listTransferredDisease, 5, pageIndex);

                    return View(model);
                }
                else
                {
                    var listTransferredDisease = await _transferredDiseaseService
                        .GetPeriodtransferredDiseaseAsync(SearchDateStart, SearchDateEnd, userDto.Id);
                    var model = PagingList.Create(listTransferredDisease, 5, pageIndex);

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
                var diseaseDto = await _transferredDiseaseService.GetAllDiseaseAsync();

                var model = new TransferredDiseaseModel();
                model.AppointmentId = id;
                var formTransferredDisease = new FormOfTransferredDisease();
                model.DiseaseList = new SelectList(diseaseDto, "Id", "NameOfDisease");
                model.FormOfTransferredDiseaseList = formTransferredDisease;

                model.ReturnUrl = Request.Headers["Referer"].ToString();
                if (model.ReturnUrl == "https://localhost:7068/TransferredDisease/CreateDisease")
                {
                    model.ReturnUrl = "https://localhost:7068/TransferredDisease/Index";
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
        public async Task<IActionResult> Edit(int id, int diseaseDtoId, DateTime dateOfDisease, DateTime dateOfRecovery)
        {
            try
            {

                var diseaseDto = await _transferredDiseaseService.GetAllDiseaseAsync();

                var model = new TransferredDiseaseModel();
                model.AppointmentId = id;
                var formTransferredDisease = new FormOfTransferredDisease();
                model.FormOfTransferredDiseaseList = formTransferredDisease;
                model.DiseaseList = new SelectList(diseaseDto, "Id", "NameOfDisease", diseaseDtoId);
                model.DiseaseId = diseaseDtoId;
                model.DateOfDisease = dateOfDisease;
                model.DateOfRecovery = dateOfRecovery;
                model.ReturnUrl = Request.Headers["Referer"].ToString();

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TransferredDiseaseModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var dto = await _transferredDiseaseService.GetTransferredDiseaseByIdAsync(model.Id);
                    dto.DateOfDisease = model.DateOfDisease;
                    dto.DateOfRecovery = model.DateOfRecovery;
                    dto.DiseaseId = model.DiseaseId;
                    dto.TypeOfTreatment = model.TypeOfTreatment;
                    dto.FormOfTransferredDiseaseDto = model.FormOfTransferredDiseaseList;

                    await _transferredDiseaseService.UpdateTransferredDiseaseAsync(dto, dto.Id);

                    return Redirect(model.ReturnUrl);
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
        public async Task<IActionResult> Create(TransferredDiseaseModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var emailUser = HttpContext.User.Identity.Name;
                    var userDto = await _userService.GetUserByEmailAsync(emailUser);
                    model.UserId = userDto.Id;

                    var dto = _mapper.Map<TransferredDiseaseDto>(model);
                    var transferredDiseaseLastId = await _transferredDiseaseService.CreateTransferredDiseaseAsync(dto);

                    if (model.AppointmentId != null)
                    {
                        var dtoDoctorVisit = await _doctorVisitService.GetDoctorVisitByIdAsync(model.AppointmentId);
                        dtoDoctorVisit.TransferredDiseaseId = transferredDiseaseLastId;
                        await _doctorVisitService.UpdateDoctorVisitAsync(dtoDoctorVisit, dtoDoctorVisit.Id);
                    }
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
        public IActionResult CreateDisease()
        {
            try
            {
                var model = new DiseaseModel();
                model.ReturnUrl = Request.Headers["Referer"].ToString();

                return View(model);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDisease(DiseaseModel model)
        {
            try
            {
                var dto = _mapper.Map<DiseaseDto>(model);
                await _transferredDiseaseService.CreateDiseaseAsync(dto);

                return Redirect(model.ReturnUrl);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsTransferredDiseasePartialView(int id)
        {
            try
            {
                var dto = await _transferredDiseaseService.GetTransferredDiseaseByIdAsync(id);

                return PartialView(dto);
            }
            catch (Exception e)
            {
                Log.Error($"{e.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id, string nameOfDisease)
        {
            try
            {
                var model = new TransferredDiseaseModel()
                {
                    Id = id,
                    NameOfDisease = nameOfDisease
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
                await _transferredDiseaseService.DeleteTransferredDiseaseAsync(id);

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
