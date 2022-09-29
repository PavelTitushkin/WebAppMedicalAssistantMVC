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
                .ForMember(dto => dto.DiseasesDto, opt => opt.MapFrom(transferredDisease => transferredDisease.Diseases));
        }
    }
}
