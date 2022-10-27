using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;
using WebAppMedicalAssistantMVC.Models;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class MedicalExaminationProfile:Profile
    {
        public MedicalExaminationProfile()
        {
            CreateMap<MedicalExamination, MedicalExaminationDto>();

            CreateMap<MedicalExaminationModel, MedicalExaminationDto>()
                .ForMember(dto => dto.MedicalInstitutionId, opt => opt.MapFrom(model => model.MedicalInstitutionId))
                .ForMember(dto => dto.AppointmentId, opt => opt.MapFrom(model => model.AppointmentId));

            CreateMap<MedicalExaminationDto, MedicalExamination>();
        }
    }
}
