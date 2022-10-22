using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class PhysicalTherapyProfile : Profile
    {
        public PhysicalTherapyProfile()
        {
            CreateMap<PhysicalTherapyModel, PhysicalTherapyDto>()
                .ForMember(dto => dto.AppointmentId, opt => opt.MapFrom(model => model.AppointmentId));

            CreateMap<PhysicalTherapyDto, PhysicalTherapy>();

            CreateMap<PhysicalTherapy, PhysicalTherapyDto>()
                .ForMember(dto => dto.MedicalInstitutionDto, opt => opt.MapFrom(entity => entity.MedicalInstitution));
        }

    }
}
