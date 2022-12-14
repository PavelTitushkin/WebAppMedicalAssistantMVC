using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class VaccinacionProfile : Profile
    {
        public VaccinacionProfile()
        {
            CreateMap<Vaccination, VaccinationDto>();

            CreateMap<VaccinationDto, Vaccination>();
        }
    }
}
