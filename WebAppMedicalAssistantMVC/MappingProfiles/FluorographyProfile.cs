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
                //.ForMember(dto => dto.DataOfExamination, opt => opt.MapFrom(fluorography => fluorography.DataOfExamination))
                //.ForMember(dto => dto.EndDateOfSurvey, opt => opt.MapFrom(fluorography => fluorography.EndDateOfSurvey))
                //.ForMember(dto => dto.NumberFluorography, opt => opt.MapFrom(fluorography => fluorography.NumberFluorography))
                //.ForMember(dto => dto.Result, opt => opt.MapFrom(fluorography => fluorography.Result))
                .ForMember(dto => dto.MedicalInstitutionDto, opt => opt.MapFrom(fluorography => fluorography.MedicalInstitution));
        }
    }
}
