using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;

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

        public async Task<List<VaccinationDto>> GetAllVaccinationsAsync()
        {
            try
            {
                var listVaccination = await _unitOfWork.Vaccination
                    .Get().Include(dto => dto.MedicalInstitutions)
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
