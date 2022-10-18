using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class AnalysisProfile : Profile
    {
        public AnalysisProfile()
        {
            CreateMap<Analysis, AnalysisDto>()
                .ForMember(dto => dto.MedicalInstitutionDto, opt => opt.MapFrom(analysis => analysis.MedicalInstitution));

            CreateMap<AnalysisModel, AnalysisDto>()
                .ForMember(dto => dto.MedicalInstitutionDtoId, opt => opt.MapFrom(model => model.MedicalInstitutionId))
                .ForMember(dto => dto.AppointmentId, opt => opt.MapFrom(model => model.AppointmentId));

            CreateMap<AnalysisDto, Analysis>()
                .ForMember(entity => entity.MedicalInstitutionId, opt => opt.MapFrom(dto => dto.MedicalInstitutionDtoId));

        }
    }
}
