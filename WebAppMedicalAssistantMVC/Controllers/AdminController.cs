using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppMedicalAssistant_Bussines.ServicesImplementations;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IMedicalInstitutionService _medicalInstitutionService;
        private readonly IMedicineService _medicineService;

        public AdminController(IMapper mapper, IUserService userService, IMedicalInstitutionService medicalInstitutionService, IMedicineService medicineService)
        {
            _mapper = mapper;
            _userService = userService;
            _medicalInstitutionService = medicalInstitutionService;
            _medicineService = medicineService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var usersDto = await _userService.GetAllUserAsync();
                
                return View(usersDto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetMedicalInstitutions()
        {
            try
            {
                var medicalInstituyionsDto = await _medicalInstitutionService.GetMedicalInstitutionsAsync();

                return View(medicalInstituyionsDto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpGet]
        public IActionResult ShowDeleteUser(int id, string userEmail)
        {
            try
            {
                var model = new UserModel();
                model.Email = userEmail;
                model.Id = id;

                return View(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult ShowDeleteMedicalInstitution(int id, string nameMedicalInstitution)
        {
            try
            {
                var model = new MedicalInstitutionModel();
                model.NameMedicalInstitution = nameMedicalInstitution;
                model.Id = id;

                return View(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ShowDeleteMedicine(int id, string NameOfMedicine)
        {
            try
            {
                var model = new MedicineModel();
                model.NameOfMedicine = NameOfMedicine;
                model.Id = id;

                return View(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ShowEditMedicalInstitution(int id, string nameMedicalInstitution, string adress, string? operatingMode, string? contact)
        {
            try
            {
                var model = new MedicalInstitutionModel();
                model.NameMedicalInstitution = nameMedicalInstitution;
                model.Adress = adress;
                model.OperatingMode = operatingMode;
                model.Contact = contact;
                model.Id = id;

                return View(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ShowEditMedicine(int id, string nameMedicine, string? linkToInstructions)
        {
            try
            {
                var model = new MedicineModel();
                model.NameOfMedicine = nameMedicine;
                model.LinkToInstructions = linkToInstructions;
                model.Id = id;

                return View(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);

                return RedirectToAction("GetUsers");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMedicalInstitution(int id)
        {
            try
            {
                await _medicalInstitutionService.DeleteMedicalInstitutionAsync(id);

                return RedirectToAction("GetMedicalInstitutions");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            try
            {
                await _medicineService.DeleteMedicineAsync(id);

                return RedirectToAction("GetMedicines");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditMedicalInstitution(MedicalInstitutionModel model)
        {
            try
            {
                var dto = await _medicalInstitutionService.GetByIdMedicalInstitutionAsync(model.Id);
                dto.NameMedicalInstitution = model.NameMedicalInstitution;
                dto.Adress = model.Adress;
                dto.OperatingMode = model.OperatingMode;
                dto.Contact = model.Contact;
                
                await _medicalInstitutionService.UpdateMedicalInstitutionAsync(dto, dto.Id);

                return RedirectToAction("GetMedicalInstitutions");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditMedicine(MedicineModel model)
        {
            try
            {
                var dto = await _medicineService.GetByIdMedicineAsync(model.Id);
                dto.NameOfMedicine = model.NameOfMedicine;
                dto.LinkToInstructions = model.LinkToInstructions;

                await _medicineService.UpdateMedicineAsync(dto, dto.Id);

                return RedirectToAction("GetMedicines");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicines()
        {
            try
            {
                var listDto = await _medicineService.GetAllMedicineAsync();

                return View(listDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult AddMedicine()
        {
            try
            {
                var model = new MedicineModel();

                return View();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicine(MedicineModel model)
        {
            try
            {
                var listLinkMedicine = _medicineService.SearchMedicineInTabletkaByAsync(model.NameOfMedicine);
                if(listLinkMedicine != null)
                {
                    var result = await _medicineService.AddMedicine(listLinkMedicine);

                    return RedirectToAction("GetMedicines");
                }

                return View();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
