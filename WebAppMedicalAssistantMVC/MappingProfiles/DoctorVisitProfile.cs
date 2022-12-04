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
                .ForMember(dto => dto.AppointmentId, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.Appointment, opt => opt.MapFrom(entity => entity.Appointment));

            CreateMap<DoctorVisitModel, DoctorVisitDto>()
                .ForMember(dto => dto.PriceVisit, opt => opt.MapFrom(model => model.PriceVisit))
                .ForMember(dto => dto.UserDtoId, opt => opt.MapFrom(model => model.UserId));

            CreateMap<DoctorVisitDto, DoctorVisit>()
                .ForMember(entity => entity.UserId, opt => opt.MapFrom(dto => dto.UserDtoId));
                
        }
    }
}
