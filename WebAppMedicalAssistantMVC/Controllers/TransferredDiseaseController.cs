using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Permissions;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
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
        public async Task<IActionResult> Index()
        {
            try
            {
                var emailUser = HttpContext.User.Identity.Name;
                var userDto = await _userService.GetUserByEmailAsync(emailUser);
                var listTransferredDisease = await _transferredDiseaseService.GetAllTransferredDiseaseAsync(userDto.Id);

                return View(listTransferredDisease);
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int idDoctorvisit, int idTransferredDisease)
        {
            try
            {

                var diseaseDto = await _transferredDiseaseService.GetAllDiseaseAsync();
                var transferredDiseaseDto = await _transferredDiseaseService
                    .GetTransferredDiseaseByIdAsync(idTransferredDisease);

                var model = new TransferredDiseaseModel();
                model.AppointmentId = idDoctorvisit;
                model.Id = idTransferredDisease;
                var formTransferredDisease = new FormOfTransferredDisease();
                model.FormOfTransferredDiseaseList = formTransferredDisease;
                model.DiseaseList = new SelectList(diseaseDto, "Id", "NameOfDisease", transferredDiseaseDto.DiseaseId);
                model.DiseaseId = transferredDiseaseDto.DiseaseId;
                model.ReturnUrl = Request.Headers["Referer"].ToString();
                //if (model.ReturnUrl == "https://localhost:7068/TransferredDisease/CreateDisease")
                //{
                //    model.ReturnUrl = "https://localhost:7068/TransferredDisease/Index";
                //}

                return View(model);

            }
            catch (Exception)
            {
                throw;
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
                    dto.DiseaseId = model.DiseaseId;
                    dto.TypeOfTreatment = model.TypeOfTreatment;
                    dto.FormOfTransferredDiseaseDto = model.FormOfTransferredDiseaseList;

                    await _transferredDiseaseService.UpdateTransferredDiseaseAsync(dto, dto.Id);

                    return Redirect(model.ReturnUrl);
                }

                return View(model);
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
        }


    }
}
