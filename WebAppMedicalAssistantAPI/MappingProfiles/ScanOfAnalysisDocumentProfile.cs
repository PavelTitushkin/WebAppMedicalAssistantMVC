using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class ScanOfAnalysisDocumentProfile : Profile
    {
        public ScanOfAnalysisDocumentProfile()
        {
            CreateMap<ScanOfAnalysisDocumentDto, ScanOfAnalysisDocument>();

            CreateMap<ScanOfAnalysisDocument, ScanOfAnalysisDocumentDto>();

        }
    }
}
