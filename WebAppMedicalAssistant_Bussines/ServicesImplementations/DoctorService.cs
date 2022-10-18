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
    public class DoctorService : IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DoctorDto>> GetAllDoctorAsync()
        {
            try
            {
                var doctorsList = await _unitOfWork.Doctor.Get()
                    .Select(entity => _mapper.Map<DoctorDto>(entity))
                    .ToListAsync();

                return doctorsList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
