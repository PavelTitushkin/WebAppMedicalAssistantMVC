using AutoMapper;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_DataBase.Entities;

namespace WebAppMedicalAssistantMVC.MappingProfiles
{
    public class AppointmentProfile:Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(opt => opt.Id))
                .ForMember(dto => dto.DescriptionOfDestination, opt => opt.MapFrom(entity => entity.DescriptionOfDestination))
                .ForMember(dto => dto.PrescribedMedicationsDto, opt => opt.MapFrom(entity => entity.PrescribedMedications))
                .ForMember(dto => dto.AnalysisDto, opt => opt.MapFrom(entity => entity.Analysis))
                .ForMember(dto => dto.PhysicalTherapysDto, opt => opt.MapFrom(entity => entity.PhysicalTherapys))
                .ForMember(dto => dto.MedicalExaminationsDto, opt => opt.MapFrom(entity => entity.MedicalExaminations));
        }
    }
}
