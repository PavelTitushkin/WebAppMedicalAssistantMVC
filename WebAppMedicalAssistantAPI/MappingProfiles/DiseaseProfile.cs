using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class DiseaseProfile : Profile
    {
        public DiseaseProfile()
        {
            CreateMap<Disease, DiseaseDto>();

            CreateMap<DiseaseDto, Disease>();
        }
    }
}
