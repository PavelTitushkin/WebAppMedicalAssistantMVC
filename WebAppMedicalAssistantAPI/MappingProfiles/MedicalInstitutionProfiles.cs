using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class MedicalInstitutionProfiles : Profile
    {
        public MedicalInstitutionProfiles()
        {
            CreateMap<MedicalInstitution, MedicalInstitutionDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(medicalInstitution => medicalInstitution.Id))
                .ForMember(dto => dto.NameMedicalInstitution, opt => opt.MapFrom(medInst => medInst.NameMedicalInstitution))
                .ForMember(dto => dto.Adress, opt => opt.MapFrom(medInst => medInst.Adress))
                .ForMember(dto => dto.OperatingMode, opt => opt.MapFrom(medInst => medInst.OperatingMode))
                .ForMember(dto => dto.Contact, opt => opt.MapFrom(medInst => medInst.Contact));

            CreateMap<MedicalInstitutionDto, MedicalInstitution>();
        }
    }
}
