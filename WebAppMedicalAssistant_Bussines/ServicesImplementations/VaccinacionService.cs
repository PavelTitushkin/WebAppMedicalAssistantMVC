using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Bussines.ServicesImplementations
{
    public class VaccinacionService : IVaccinationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public VaccinacionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateVaccinationAsync(VaccinationDto dto)
        {
            try
            {
                var entity = _mapper.Map<Vaccination>(dto);
                await _unitOfWork.Vaccination.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<List<VaccinationDto>> GetAllVaccinationsAsync(int id)
        {
            try
            {
                var listVaccination = await _unitOfWork.Vaccination
                    .FindBy(entity=>entity.UserId.Equals(id))
                    .Include(dto => dto.MedicalInstitutions)
                    .Select(vaccination => _mapper.Map<VaccinationDto>(vaccination))
                    .ToListAsync();
                return listVaccination;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
