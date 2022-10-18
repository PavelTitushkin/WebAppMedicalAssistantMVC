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
                    .Where(user => user.UserId.Equals(userId))
                    .Include(entity => entity.MedicalInstitution)
                    .Include(entity => entity.Doctor)
                    .Include(entity => entity.TransferredDisease.Disease)
                    .Include(entity => entity.Appointment)
                    .Select(doctorVisit => _mapper.Map<DoctorVisitDto>(doctorVisit))
                    .AsNoTracking()
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
    }
}
