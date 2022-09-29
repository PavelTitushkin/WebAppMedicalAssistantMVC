using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class AppointmentProfile:Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(opt => opt.Id))
                .ForMember(dto => dto.DescriptionOfDestination, opt => opt.MapFrom(opt => opt.DescriptionOfDestination));
        }
    }
}
