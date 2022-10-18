using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class FluorographyProfile : Profile
    {
        public FluorographyProfile()
        {
            CreateMap<Fluorography, FluorographyDto>()
                .ForMember(dto => dto.MedicalInstitutionDto, opt => opt.MapFrom(fluorography => fluorography.MedicalInstitution));

            CreateMap<FluorographyModel, FluorographyDto>()
                .ForMember(dto => dto.MedicalInstitutionDtoId, opt => opt.MapFrom(model => model.MedicalInstitutionId))
                .ForMember(dto => dto.UserDtoId, opt => opt.MapFrom(model => model.UserId));

            CreateMap<FluorographyDto, Fluorography>()
                .ForMember(entity => entity.UserId, opt => opt.MapFrom(dto => dto.UserDtoId))
                .ForMember(entity => entity.MedicalInstitutionId, opt => opt.MapFrom(dto => dto.MedicalInstitutionDtoId));
        }
    }
}
