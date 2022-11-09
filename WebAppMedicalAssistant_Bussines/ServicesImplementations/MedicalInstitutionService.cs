using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_DataBase.Entities;

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

        public async Task<int> CreateMedicalInstitutionAsync(MedicalInstitutionDto dto)
        {
            try
            {
                var entity = _mapper.Map<MedicalInstitution>(dto);
                await _unitOfWork.MedicalInstitution.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteMedicalInstitutionAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.MedicalInstitution.FindBy(entity => entity.Id.Equals(id))
                    .Include(include => include.DoctorVisits)
                    .Include(include => include.Analyses)
                    .Include(include => include.Vaccinations)
                    .Include(include => include.Fluorographys)
                    .Include(include => include.MedicalExaminations)
                    .Include(include => include.PhysicalTherapies)
                    .FirstOrDefaultAsync();

                _unitOfWork.MedicalInstitution.Remove(entity);
                await _unitOfWork.Commit();
            }

            catch (Exception)
            {
                throw new ArgumentException("", nameof(id));
            }
        }

        public async Task<MedicalInstitutionDto> GetByIdMedicalInstitutionAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.MedicalInstitution
                    .FindBy(entity => entity.Id.Equals(id))
                    .Select(entity => _mapper.Map<MedicalInstitutionDto>(entity))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
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

        public async Task<int> UpdateMedicalInstitutionAsync(MedicalInstitutionDto dto, int id)
        {
            try
            {
                var sourceDto = await GetByIdMedicalInstitutionAsync(id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if (!dto.NameMedicalInstitution.Equals(sourceDto.NameMedicalInstitution))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.NameMedicalInstitution),
                            PropertyValue = dto.NameMedicalInstitution
                        });
                    }
                    if (!dto.Adress.Equals(sourceDto.Adress))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.Adress),
                            PropertyValue = dto.Adress
                        });
                    }
                    if (!dto.OperatingMode.Equals(sourceDto.OperatingMode))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.OperatingMode),
                            PropertyValue = dto.OperatingMode
                        });
                    }
                    if (!dto.Contact.Equals(sourceDto.Contact))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.Contact),
                            PropertyValue = dto.Contact
                        });
                    }
                }

                await _unitOfWork.MedicalInstitution.PatchAsync(id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
