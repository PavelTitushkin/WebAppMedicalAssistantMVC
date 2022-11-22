using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class DoctorVisitProfile : Profile
    {
        public DoctorVisitProfile()
        {
            CreateMap<DoctorVisit, DoctorVisitDto>()
                .ForMember(dto => dto.DiseaseDto, opt => opt.MapFrom(entity => entity.TransferredDisease.Disease))
                .ForMember(dto => dto.MedicalInstitutionDto, opt => opt.MapFrom(entity => entity.MedicalInstitution))
                .ForMember(dto => dto.DoctorDto, opt => opt.MapFrom(entity => entity.Doctor))
                .ForMember(dto => dto.DoctorDtoId, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.AppointmentDto, opt => opt.MapFrom(entity => entity.Appointment));

            CreateMap<DoctorVisitModel, DoctorVisitDto>()
                .ForMember(dto => dto.MedicalInstitutionDtoId, opt => opt.MapFrom(model => model.MedicalInstitutionId))
                .ForMember(dto => dto.DoctorDtoId, opt => opt.MapFrom(model => model.DoctorId))
                .ForMember(dto => dto.PriceVisit, opt => opt.MapFrom(model => model.PriceVisit))
                .ForMember(dto => dto.UserDtoId, opt => opt.MapFrom(model => model.UserId));

            CreateMap<DoctorVisitDto, DoctorVisit>()
                .ForMember(entity => entity.MedicalInstitutionId, opt => opt.MapFrom(dto => dto.MedicalInstitutionDtoId))
                .ForMember(entity => entity.DoctorId, opt => opt.MapFrom(dto => dto.DoctorDtoId))
                .ForMember(entity => entity.UserId, opt => opt.MapFrom(dto => dto.UserDtoId));
                
        }
    }
}
