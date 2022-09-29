using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class DoctorVisitProfile : Profile
    {
        public DoctorVisitProfile()
        {
            CreateMap<DoctorVisit, DoctorVisitDto>()
                .ForMember(dto => dto.TransferredDiseaseDto, opt => opt.MapFrom(transferredDisease => transferredDisease.TransferredDisease))
                .ForMember(dto => dto.AppointmentsDto, opt => opt.MapFrom(appointments => appointments.Appointments))
                .ForMember(dto => dto.MedicalInstitutionDto, opt => opt.MapFrom(medicalInstitution => medicalInstitution.MedicalInstitution))
                .ForMember(dto=>dto.DoctorDto, opt=>opt.MapFrom(doctor => doctor.Doctor));
        }
    }
}
