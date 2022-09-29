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
    public class DoctorVisitService : IDoctorVisitService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorVisitService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DoctorVisitDto>> GetAllDoctorVisitAsync()
        {
            try
            {
                var listDoctorVisit = await _unitOfWork.DoctorVisit.Get()
                    .Include(dto => dto.Appointments)
                    .Include(dto => dto.TransferredDisease.Diseases)
                    .Include(dto=>dto.MedicalInstitution)
                    .Include(dto=>dto.Doctor)
                    .Select(doctorVisit => _mapper.Map<DoctorVisitDto>(doctorVisit))
                    .ToListAsync();

                return listDoctorVisit;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
