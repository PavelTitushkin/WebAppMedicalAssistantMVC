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
    public class DoctorService : IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateDoctorAsync(DoctorDto dto)
        {
            try
            {
                var entity = _mapper.Map<Doctor>(dto);
                await _unitOfWork.Doctor.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
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

        public async Task<DoctorDto?> GetDoctorByIdAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.Doctor
                    .FindBy(entity => entity.Id == id)
                    .AsNoTracking()
                    .Select(entity => _mapper.Map<DoctorDto>(entity))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
