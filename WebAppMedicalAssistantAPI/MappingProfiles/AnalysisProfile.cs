using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class AnalysisProfile : Profile
    {
        public AnalysisProfile()
        {
            CreateMap<Analysis, AnalysisDto>();

            CreateMap<AnalysisDto, Analysis>();
        }
    }
}
