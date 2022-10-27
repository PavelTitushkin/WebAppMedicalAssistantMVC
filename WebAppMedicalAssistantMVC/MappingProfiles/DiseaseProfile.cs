using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class DiseaseProfile:Profile
    {
        public DiseaseProfile()
        {
            CreateMap<Disease, DiseaseDto>();

            CreateMap<DiseaseModel, DiseaseDto>();

            CreateMap<DiseaseDto, Disease>();
        }
    }
}
