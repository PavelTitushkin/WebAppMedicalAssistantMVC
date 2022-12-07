using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAppMedicalAssistant_Core;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistant_Bussines.ServicesImplementations
{
    public class VaccinacionService : IVaccinationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public VaccinacionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateVaccinationAsync(VaccinationDto dto)
        {
            try
            {
                var entity = _mapper.Map<Vaccination>(dto);
                await _unitOfWork.Vaccination.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task DeleteVaccinationAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.Vaccination
                    .FindBy(entity => entity.Id == id)
                    .FirstOrDefaultAsync();
                _unitOfWork.Vaccination.Remove(entity);
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<VaccinationDto>> GetAllVaccinationsAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.Vaccination
                    .FindBy(entity => entity.UserId.Equals(id))
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitutions)
                    .OrderBy(entity => entity.DateOfVaccination)
                    .Select(vaccination => _mapper.Map<VaccinationDto>(vaccination))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<VaccinationDto>> GetPeriodVaccinationAsync(DateTime searchDateStart, DateTime searchDateEnd, int id)
        {
            try
            {
                var dto = await _unitOfWork.Vaccination
                    .FindBy(entity => entity.UserId.Equals(id))
                    .Where(entity => entity.DateOfVaccination >= searchDateStart && entity.DateOfVaccination <= searchDateEnd)
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitutions)
                    .OrderBy(entity => entity.DateOfVaccination)
                    .Select(vaccination => _mapper.Map<VaccinationDto>(vaccination))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VaccinationDto> GetVaccinationByIdAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.Vaccination
                    .FindBy(entity => entity.Id.Equals(id))
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitutions)
                    .Select(vaccination => _mapper.Map<VaccinationDto>(vaccination))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateVaccinationAsync(VaccinationDto dto, int id)
        {
            try
            {
                var sourceDto = await GetVaccinationByIdAsync(id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if (dto.ApplicationOfVaccine != sourceDto.ApplicationOfVaccine)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.ApplicationOfVaccine),
                            PropertyValue = dto.ApplicationOfVaccine
                        });
                    }
                    if (dto.NameOfVaccine != sourceDto.NameOfVaccine)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.NameOfVaccine),
                            PropertyValue = dto.NameOfVaccine
                        });
                    }
                    if (dto.VacineSeria != sourceDto.VacineSeria)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.VacineSeria),
                            PropertyValue = dto.VacineSeria
                        });
                    }
                    if (dto.VacineDose != sourceDto.VacineDose)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.VacineDose),
                            PropertyValue = dto.VacineDose
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
                await _unitOfWork.Vaccination.PatchAsync(dto.Id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
