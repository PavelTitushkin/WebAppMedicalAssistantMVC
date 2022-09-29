﻿using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class MedicalExaminationProfile:Profile
    {
        public MedicalExaminationProfile()
        {
            CreateMap<MedicalExamination, MedicalExaminationDto>()
                .ForMember(dto => dto.AppointmentDto, opt => opt.MapFrom(opt => opt.Appointment));
        }
    }
}
