using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantAPI.Model.Requests;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class DoctorVisitProfile : Profile
    {
        public DoctorVisitProfile()
        {
            CreateMap<DoctorVisit, DoctorVisitDto>()
                .ForMember(dto => dto.DiseaseDto, opt => opt.MapFrom(entity => entity.TransferredDisease.Disease))
                .ForMember(dto => dto.AppointmentId, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Appointment, opt => opt.MapFrom(entity => entity.Appointment));

            CreateMap<DoctorVisitDto, DoctorVisit>()
                .ForMember(entity => entity.UserId, opt => opt.MapFrom(dto => dto.UserDtoId));

            CreateMap<DoctorVisitRequestModel, DoctorVisitDto>();
                
        }
    }
}
