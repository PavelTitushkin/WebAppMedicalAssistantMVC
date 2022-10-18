using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;

namespace WebAppMedicalAssistant_Bussines.ServicesImplementations
{
    public class MedicalInstitutionService : IMedicalInstitutionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MedicalInstitutionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MedicalInstitutionDto>> GetMedicalInstitutionsAsync()
        {
            try
            {
                var listMedicalInstitution = await _unitOfWork.MedicalInstitution
                    .Get()
                    .Select(entity => _mapper.Map<MedicalInstitutionDto>(entity))
                    .ToListAsync();

                return listMedicalInstitution;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
