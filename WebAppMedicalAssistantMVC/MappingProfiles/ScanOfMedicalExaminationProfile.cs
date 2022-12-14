using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class ScanOfMedicalExaminationProfile : Profile
    {
        public ScanOfMedicalExaminationProfile()
        {
            CreateMap<ScanOfMedicalExamination, ScanOfMedicalExaminationDto>();

            CreateMap<ScanOfMedicalExaminationDto, ScanOfMedicalExamination>();
        }
    }
}
