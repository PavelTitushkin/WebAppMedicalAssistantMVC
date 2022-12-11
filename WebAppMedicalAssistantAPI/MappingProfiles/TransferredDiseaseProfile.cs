using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class TransferredDiseaseProfile:Profile
    {
        public TransferredDiseaseProfile()
        {
            CreateMap<TransferredDisease, TransferredDiseaseDto>()
                .ForMember(dto => dto.NameOfDisease, opt => opt.MapFrom(entity => entity.Disease.NameOfDisease));

            CreateMap<TransferredDiseaseDto, TransferredDisease>();
        }
    }
}
