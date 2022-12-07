using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class VaccinacionProfile : Profile
    {
        public VaccinacionProfile()
        {
            CreateMap<Vaccination, VaccinationDto>();

            CreateMap<VaccinationDto, Vaccination>();

            CreateMap<VaccinacionModel, VaccinationDto>();
        }
    }
}
