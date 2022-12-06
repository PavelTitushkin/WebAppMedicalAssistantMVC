using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_DataBase;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Bussines.ServicesImplementations
{
    public class FluorographyService : IFluorographyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FluorographyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreatefluorographyAsync(FluorographyDto fluorographyDto)
        {
            try
            {
                var entity = _mapper.Map<Fluorography>(fluorographyDto);
                await _unitOfWork.Fluorography.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task DeleteFluorographyAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.Fluorography
                    .FindBy(entity => entity.Id == id).FirstOrDefaultAsync();
                _unitOfWork.Fluorography.Remove(entity);
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FluorographyDto>> GetAllFluorographiesAsync(int userId) 
        {
            try
            {
                var listFlyorographies = await _unitOfWork.Fluorography
                    .Get().AsNoTracking().Where(entity => entity.UserId.Equals(userId))
                    .Include(include => include.MedicalInstitution)
                    .OrderBy(entity=> entity.DataOfExamination)
                    .Select(flyoragraphies => _mapper.Map<FluorographyDto>(flyoragraphies))
                    .ToListAsync();

                
                return listFlyorographies;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FluorographyDto> GetFluorographyByIdAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.Fluorography
                    .Get().Where(entity => entity.Id==id)
                    .AsNoTracking()
                    .Include(dto => dto.MedicalInstitution)
                    .OrderBy(entity => entity.DataOfExamination)
                    .Select(entity => _mapper.Map<FluorographyDto>(entity))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FluorographyDto>> GetPeriodfluorographyAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int userId)
        {
            try
            {
                var dto = await _unitOfWork.Fluorography
                    .FindBy(entity => entity.UserId.Equals(userId))
                    .AsNoTracking()
                    .Where(entity => entity.DataOfExamination >= SearchDateStart && entity.DataOfExamination <= SearchDateEnd)
                    .Include(include => include.MedicalInstitution)
                    .OrderBy(entity => entity.DataOfExamination)
                    .Select(entity => _mapper.Map<FluorographyDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateFluorographyAsync(FluorographyDto dto, int id)
        {
            try
            {
                var sourceDto = await GetFluorographyByIdAsync(id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if(dto.DataOfExamination != sourceDto.DataOfExamination)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.DataOfExamination),
                            PropertyValue = dto.DataOfExamination
                        });
                    }
                    if (dto.EndDateOfSurvey != sourceDto.EndDateOfSurvey)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.EndDateOfSurvey),
                            PropertyValue = dto.EndDateOfSurvey
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
                    if (dto.NumberFluorography != sourceDto.NumberFluorography)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.NumberFluorography),
                            PropertyValue = dto.NumberFluorography
                        });
                    }
                    if (dto.Result != sourceDto.Result)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.Result),
                            PropertyValue = dto.Result
                        });
                    }

                }
                await _unitOfWork.Fluorography.PatchAsync(dto.Id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
