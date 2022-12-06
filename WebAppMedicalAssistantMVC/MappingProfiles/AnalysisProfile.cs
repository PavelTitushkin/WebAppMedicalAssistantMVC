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
            CreateMap<Analysis, AnalysisDto>();

            CreateMap<AnalysisModel, AnalysisDto>();

            CreateMap<AnalysisDto, Analysis>();
        }
    }
}
