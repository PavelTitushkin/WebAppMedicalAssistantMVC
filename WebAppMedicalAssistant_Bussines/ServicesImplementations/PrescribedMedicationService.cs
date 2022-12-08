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
                var dto = await _unitOfWork.PrescribedMedication
                    .Get().Where(entity => entity.UserId.Equals(id))
                    .AsNoTracking()
                    .Include(include => include.Medicine)
                    .OrderBy(entity => entity.StartDateOfMedication)
                    .Select(entity => _mapper.Map<PrescribedMedicationDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<PrescribedMedicationDto> GetPrescribedMedicationByIdAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.PrescribedMedication
                    .Get().Where(entity => entity.Id.Equals(id))
                    .AsNoTracking()
                    .Include(include => include.Medicine)
                    .Select(entity => _mapper.Map<PrescribedMedicationDto>(entity))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<List<PrescribedMedicationDto>> GetPeriodPrescribedMedicationAsync(DateTime searchDateStart, DateTime searchDateEnd, int id)
        {
            try
            {
                var dto = await _unitOfWork.PrescribedMedication
                    .FindBy(entity => entity.UserId == id)
                    .Where(entity => entity.StartDateOfMedication >= searchDateStart && entity.EndDateOfMedication <= searchDateStart)
                    .AsNoTracking()
                    .Include(include => include.Medicine)
                    .OrderBy(entity => entity.StartDateOfMedication)
                    .Select(entity => _mapper.Map<PrescribedMedicationDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<int> CreatePrescribedMedicationAsync(PrescribedMedicationDto dto)
        {
            try
            {
                var entity = _mapper.Map<PrescribedMedication>(dto);
                entity.Id = 0;
                await _unitOfWork.PrescribedMedication.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<int> UpdatePrescribedMedicationAsync(PrescribedMedicationDto dto, int id)
        {
            try
            {
                var sourceDto = await GetPrescribedMedicationByIdAsync(id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if (dto.StartDateOfMedication != sourceDto.StartDateOfMedication)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.StartDateOfMedication),
                            PropertyValue = dto.StartDateOfMedication
                        });
                    }
                    if (dto.EndDateOfMedication != sourceDto.EndDateOfMedication)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.EndDateOfMedication),
                            PropertyValue = dto.EndDateOfMedication
                        });
                    }
                    if (dto.MedicineId != sourceDto.MedicineId)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.MedicineId),
                            PropertyValue = dto.MedicineId
                        });
                    }
                    if (dto.MedicinePrice != sourceDto.MedicinePrice)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.MedicinePrice),
                            PropertyValue = dto.MedicinePrice
                        });
                    }
                    if (dto.MedicineDose != sourceDto.MedicineDose)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.MedicineDose),
                            PropertyValue = dto.MedicineDose
                        });
                    }
                }

                await _unitOfWork.PrescribedMedication.PatchAsync(id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeletePrescribedMedicationAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.PrescribedMedication
                    .FindBy(entity => entity.Id == id)
                    .FirstOrDefaultAsync();
                _unitOfWork.PrescribedMedication.Remove(entity);

                await _unitOfWork.Commit();
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
