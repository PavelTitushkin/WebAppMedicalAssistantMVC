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
                //.ForMember(dto => dto.MedicalInstitutions, opt => opt.MapFrom(vaccination => vaccination.MedicalInstitutions));

            CreateMap<VaccinacionModel, VaccinationDto>();

            CreateMap<VaccinationDto, Vaccination>();
        }
    }
}
