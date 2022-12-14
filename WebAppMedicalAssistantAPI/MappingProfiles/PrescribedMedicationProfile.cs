using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class PrescribedMedicationProfile : Profile
    {
        public PrescribedMedicationProfile()
        {
            CreateMap<PrescribedMedication, PrescribedMedicationDto>();

            CreateMap<PrescribedMedicationDto, PrescribedMedication>();
        }
    }
}
