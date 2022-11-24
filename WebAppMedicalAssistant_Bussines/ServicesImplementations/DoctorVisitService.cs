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
    public class DoctorVisitService : IDoctorVisitService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorVisitService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateDoctorVisitAsync(DoctorVisitDto doctorVisitDto)
        {
            try
            {
                var entity = _mapper.Map<DoctorVisit>(doctorVisitDto);
                await _unitOfWork.DoctorVisit.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DoctorVisitDto>> GetAllDoctorVisitAsync(int userId)
        {
            try
            {
                var listDoctorVisit = await _unitOfWork.DoctorVisit.Get()
                    .AsNoTracking()
                    .Where(user => user.UserId.Equals(userId))
                    .Include(entity => entity.MedicalInstitution)
                    .Include(entity => entity.Doctor)
                    .Include(entity => entity.TransferredDisease)
                    .ThenInclude(tranDis=>tranDis.Disease)
                    .Include(entity => entity.Appointment)
                    .OrderBy(entity=> entity.DateVisit)
                    .Select(doctorVisit => _mapper.Map<DoctorVisitDto>(doctorVisit))
                    .ToListAsync();

                return listDoctorVisit;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AppointmentDto> GetAppointmentAsync(int doctorVisitId)
        {
            try
            {

                var appointmentDto = await _unitOfWork.Appointment
                    .FindBy(entity => entity.Id.Equals(doctorVisitId))
                    .Include(entity => entity.PrescribedMedications).ThenInclude(presMed => presMed.Medicine)
                    .Include(entity => entity.Analysis)
                    .Include(entity => entity.MedicalExaminations)
                    .Include(entity => entity.PhysicalTherapys)
                    .Select(entity => _mapper.Map<AppointmentDto>(entity))
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return appointmentDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateAppointment(int id)
        {
            try
            {
                await _unitOfWork.Appointment.AddEntityAsync(new Appointment { Id = id });
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<DoctorVisitDto> GetDoctorVisitByIdAsync(int? appontmentId)
        {
            try
            {
                var doctorVisit = await _unitOfWork.DoctorVisit
                    .FindBy(entity => entity.Id.Equals(appontmentId))
                    .Select(entity =>_mapper.Map<DoctorVisitDto>(entity))
                    .FirstOrDefaultAsync();

                return doctorVisit;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateDoctorVisitAsync(DoctorVisitDto dto, int dtoId)
        {
            try
            {
                var sourceDto = await GetDoctorVisitByIdAsync(dtoId);
                var patchList = new List<PatchModel>();
                if(dto != null)
                {
                    if (!dto.TransferredDiseaseId.Equals(sourceDto.TransferredDiseaseId))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.TransferredDiseaseId),
                            PropertyValue = dto.TransferredDiseaseId
                        });
                    }
                    if (!dto.DateVisit.Equals(sourceDto.DateVisit))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.DateVisit),
                            PropertyValue = dto.DateVisit
                        });
                    }
                    if (!dto.MedicalInstitutionDtoId.Equals(sourceDto.MedicalInstitutionDtoId))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.MedicalInstitutionDtoId),
                            PropertyValue = dto.MedicalInstitutionDtoId
                        });
                    }
                    if (!dto.DoctorDtoId.Equals(sourceDto.DoctorDtoId))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.DoctorDtoId),
                            PropertyValue = dto.DoctorDtoId
                        });
                    }
                    if (!dto.PriceVisit.Equals(sourceDto.PriceVisit))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.PriceVisit),
                            PropertyValue = dto.PriceVisit
                        });
                    }
                }

                await _unitOfWork.DoctorVisit.PatchAsync(dtoId, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }      
        }

        public async Task<int> UpdateAppointmentAsync(AppointmentDto dto, int dtoId)
        {
            try
            {
                var sourceDto = await GetAppointmentAsync(dtoId);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    patchList.Add(new PatchModel()
                    {
                        PropertyName = nameof(dto.DescriptionOfDestination),
                        PropertyValue = dto.DescriptionOfDestination
                    });
                }

                await _unitOfWork.Appointment.PatchAsync(dtoId, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DoctorVisitDto>> GetPeriodDoctorVisitAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int userId)
        {
            try
            {
                var listDoctorVisit = await _unitOfWork.DoctorVisit
                    .FindBy(entity => entity.UserId == userId)
                    .AsNoTracking()
                    .Where(entityData => entityData.DateVisit >= SearchDateStart && entityData.DateVisit <= SearchDateEnd)
                    .Include(entity => entity.MedicalInstitution)
                    .Include(entity => entity.Doctor)
                    .Include(entity => entity.TransferredDisease)
                    .ThenInclude(tranDis => tranDis.Disease)
                    .Include(entity => entity.Appointment)
                    .OrderBy(entity => entity.DateVisit)
                    .Select(doctorVisit => _mapper.Map<DoctorVisitDto>(doctorVisit))
                    .ToListAsync();

                return listDoctorVisit;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteDoctorVisitAsync(int id)
        {
            try
            {
                var entityDoctorVisit = await _unitOfWork.DoctorVisit
                    .FindBy(entity => entity.Id.Equals(id))
                    .Include(include => include.Appointment)
                    .ThenInclude(i=>i.Analysis)
                    .Include(include=>include.MedicalInstitution)
                    .Include(include => include.TransferredDisease)
                    .Include(include => include.Doctor)
                    .Include(include => include.User)
                    .FirstOrDefaultAsync();
                _unitOfWork.DoctorVisit.Remove(entityDoctorVisit);

                await _unitOfWork.Commit();
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
