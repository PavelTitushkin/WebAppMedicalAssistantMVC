using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class FluorographyProfile : Profile
    {
        public FluorographyProfile()
        {
            CreateMap<Fluorography, FluorographyDto>()
                .ForMember(dto => dto.MedicalInstitutionDto, opt => opt.MapFrom(fluorography => fluorography.MedicalInstitution));
        }
    }
}
