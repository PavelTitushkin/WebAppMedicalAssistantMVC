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
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Bussines.ServicesImplementations
{
    public class PrescribedMedicationService : IPrescribedMedicationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PrescribedMedicationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PrescribedMedicationDto>> GetAllPrescribedMedicationsAsync(int id)
        {
            try
            {
                var prescribedMedication = await _unitOfWork.PrescribedMedication
                    .Get().Where(entity => entity.UserId.Equals(id))
                    .Include(include => include.Medicine)
                    .Select(entity => _mapper.Map<PrescribedMedicationDto>(entity))
                    .ToListAsync();

                return prescribedMedication;
            }
            catch (Exception)
            {

                throw new ArgumentException();
            }
        }
    }
}
