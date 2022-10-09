using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class PrescribedMedicationProfile : Profile
    {
        public PrescribedMedicationProfile()
        {
            CreateMap<PrescribedMedication, PrescribedMedicationDto>()
                .ForMember(dto => dto.DiseasesDto, opt => opt.MapFrom(entity => entity.TransferredDisease.Diseases))
                .ForMember(dto => dto.MedicinesDto, opt => opt.MapFrom(entity => entity.Medicine));
        }
    }
}
