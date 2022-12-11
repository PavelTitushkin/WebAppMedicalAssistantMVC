using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class PhysicalTherapyProfile : Profile
    {
        public PhysicalTherapyProfile()
        {
            CreateMap<PhysicalTherapyDto, PhysicalTherapy>();

            CreateMap<PhysicalTherapy, PhysicalTherapyDto>();
        }

    }
}
