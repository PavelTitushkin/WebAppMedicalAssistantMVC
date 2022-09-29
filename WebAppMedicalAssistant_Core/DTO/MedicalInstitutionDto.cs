namespace WebAppMedicalAssistant_Core.DTO
{
    public class MedicalInstitutionDto
    {
        public int Id { get; set; }
        public string? NameMedicalInstitution { get; set; }
        public string? Adress { get; set; }
        public string? OperatingMode { get; set; }
        public string? Contact { get; set; }

        //навигационное свойство
        public List<DoctorVisitDto>? DoctorVisitDto { get; set; }
        public List<VaccinationDto>? VaccinationDto { get; set; }

        public List<FluorographyDto>? FluorographysDto { get; set; }
        public List<AnalysisDto>? AnalysesDto { get; set; }
        public List<MedicalExaminationDto>? medicalExaminationsDto { get; set; }
        public List<PhysicalTherapyDto>? physicalTherapiesDto { get; set; }

    }
}
