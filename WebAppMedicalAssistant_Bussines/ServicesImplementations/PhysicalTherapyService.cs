using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core;
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

        public async Task<List<PhysicalTherapyDto>> GetAllPhysicalTherapyAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.PhysicalTherapy
                    .FindBy(entity => entity.UserId.Equals(id))
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitution)
                    .OrderBy(orderBy => orderBy.DatePhysicalTherapy)
                    .Select(entity => _mapper.Map<PhysicalTherapyDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PhysicalTherapyDto> GetPhysicalTherapyByIdAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.PhysicalTherapy
                    .FindBy(entity => entity.Id.Equals(id))
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitution)
                    .Select(entity => _mapper.Map<PhysicalTherapyDto>(entity))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PhysicalTherapyDto?>> GetScheduledPhysicalTherapyAsync(DateTime dateNow, int id)
        {
            try
            {
                var dto = await _unitOfWork.PhysicalTherapy
                    .FindBy(entity => entity.UserId.Equals(id))
                    .AsNoTracking()
                    .Where(entity => entity.DatePhysicalTherapy >= dateNow)
                    .Include(include => include.MedicalInstitution)
                    .OrderBy(orderBy => orderBy.DatePhysicalTherapy)
                    .Select(entity => _mapper.Map<PhysicalTherapyDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<List<PhysicalTherapyDto>> GetPeriodPhysicalTherapyAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int id)
        {
            try
            {
                var dto = await _unitOfWork.PhysicalTherapy
                    .FindBy(entity => entity.UserId.Equals(id))
                    .Where(entity => 
                    entity.DatePhysicalTherapy >= SearchDateStart && entity.DatePhysicalTherapy <= SearchDateEnd)
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitution)
                    .OrderBy(orderBy => orderBy.DatePhysicalTherapy)
                    .Select(entity => _mapper.Map<PhysicalTherapyDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> CreatePhysicalTherapyAsync(PhysicalTherapyDto dto)
        {
            try
            {
                var entity = _mapper.Map<PhysicalTherapy>(dto);
                entity.Id = 0;
                await _unitOfWork.PhysicalTherapy.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeletePhysicalTherapyAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.PhysicalTherapy
                    .FindBy(entity => entity.Id == id)
                    .FirstOrDefaultAsync();
                _unitOfWork.PhysicalTherapy.Remove(entity);
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdatePhysicalTherapyAsync(PhysicalTherapyDto dto, int id)
        {
            try
            {
                var sourceDto = await GetPhysicalTherapyByIdAsync(id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if (dto.DatePhysicalTherapy != sourceDto.DatePhysicalTherapy)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.DatePhysicalTherapy),
                            PropertyValue = dto.DatePhysicalTherapy
                        });
                    }
                    if (dto.MedicalInstitutionId != sourceDto.MedicalInstitutionId)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.MedicalInstitutionId),
                            PropertyValue = dto.MedicalInstitutionId
                        });
                    }
                }

                await _unitOfWork.PhysicalTherapy.PatchAsync(dto.Id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
