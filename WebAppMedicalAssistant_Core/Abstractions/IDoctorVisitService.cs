﻿using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IDoctorVisitService
    {
        Task<List<DoctorVisitDto>> GetAllDoctorVisitAsync(int userId);
        Task<int> CreateDoctorVisitAsync(DoctorVisitDto doctorVisitDto);
        Task<AppointmentDto> GetAppointmentAsync(int doctorVisitId);
        Task<int> CreateAppointment(int id);

    }
}
