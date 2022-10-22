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
    public class PhysicalTherapyService : IPhysicalTherapyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PhysicalTherapyService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreatePhysicalTherapyAsync(PhysicalTherapyDto dto)
        {
            try
            {
                var entity = _mapper.Map<PhysicalTherapy>(dto);
                await _unitOfWork.PhysicalTherapy.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PhysicalTherapyDto>> GetAllPhysicalTherapyAsync(int id)
        {
            try
            {
                var listPhysicalTherapy = await _unitOfWork.PhysicalTherapy
                    .FindBy(entity => entity.UserId.Equals(id))
                    .Include(include => include.MedicalInstitution)
                    .Select(entity => _mapper.Map<PhysicalTherapyDto>(entity))
                    .ToListAsync();

                return listPhysicalTherapy;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
