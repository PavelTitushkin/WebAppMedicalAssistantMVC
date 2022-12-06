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
    public class MedicalExaminationService : IMedicalExaminationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MedicalExaminationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateMedicalExaminationAsync(MedicalExaminationDto dto)
        {
            try
            {
                var entity = _mapper.Map<MedicalExamination>(dto);
                entity.Id = 0;
                await _unitOfWork.MedicalExamination.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> CreateScanOfMedicalExaminationAsync(ScanOfMedicalExaminationDto dto)
        {
            try
            {
                var entity = _mapper.Map<ScanOfMedicalExamination>(dto);
                await _unitOfWork.ScanOfMedicalExamination.AddEntityAsync(entity);

                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<MedicalExaminationDto>> GetAllMedicalExaminationAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.MedicalExamination
                    .FindBy(entity=>entity.UserId.Equals(id))
                    .AsNoTracking()
                    .Include(include=>include.MedicalInstitution)
                    .Include(include=>include.ScanOfMedicalExaminations)
                    .OrderBy(entity => entity.DateOfMedicalExamination)
                    .Select(entity => _mapper.Map<MedicalExaminationDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MedicalExaminationDto> GetMedicalExaminationByIdAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.MedicalExamination
                    .FindBy(entity => entity.Id == id)
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitution)
                    .Include(include => include.ScanOfMedicalExaminations)
                    .OrderBy(entity => entity.DateOfMedicalExamination)
                    .Select(entity => _mapper.Map<MedicalExaminationDto>(entity))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<MedicalExaminationDto>> GetPeriodMedicalExaminationAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int userId)
        {
            try
            {
                var dto = await _unitOfWork.MedicalExamination
                    .FindBy(entity => entity.UserId == userId)
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitution)
                    .Include(include => include.ScanOfMedicalExaminations)
                    .Where(entity => entity.DateOfMedicalExamination >= SearchDateStart && entity.DateOfMedicalExamination <= SearchDateEnd)
                    .OrderBy(entity => entity.DateOfMedicalExamination)
                    .Select(entity => _mapper.Map<MedicalExaminationDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ScanOfMedicalExaminationDto> GetScanOfMedicalExaminationByIdAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.ScanOfMedicalExamination
                    .FindBy(entity => entity.Id == id)
                    .AsNoTracking()
                    .Select(entity => _mapper.Map<ScanOfMedicalExaminationDto>(entity))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateMedicalExaminationAsync(MedicalExaminationDto dto, int id)
        {
            try
            {
                var sourceDto = await GetMedicalExaminationByIdAsync(id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if (dto.NameOfMedicalExamination != sourceDto.NameOfMedicalExamination)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.NameOfMedicalExamination),
                            PropertyValue = dto.NameOfMedicalExamination
                        });
                    }
                    if (dto.DateOfMedicalExamination != sourceDto.DateOfMedicalExamination)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.DateOfMedicalExamination),
                            PropertyValue = dto.DateOfMedicalExamination
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
                    if (dto.PriceOfMedicalExamination != sourceDto.PriceOfMedicalExamination)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.PriceOfMedicalExamination),
                            PropertyValue = dto.PriceOfMedicalExamination
                        });
                    }
                }

                await _unitOfWork.MedicalExamination.PatchAsync(id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<int> UpdateScanOfMedicalExaminationAsync(ScanOfMedicalExaminationDto dto)
        {
            try
            {
                var sourceDto = await GetScanOfMedicalExaminationByIdAsync(dto.Id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if (dto.ScanData != sourceDto.ScanData)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.ScanData),
                            PropertyValue = dto.ScanData
                        });
                    }
                }

                await _unitOfWork.ScanOfMedicalExamination.PatchAsync(dto.Id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteMedicalExaminationAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.MedicalExamination
                    .FindBy(entity => entity.Id == id)
                    .FirstOrDefaultAsync();
                _unitOfWork.MedicalExamination.Remove(entity);
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
