using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Appointment: IBaseEntity
    {
        [Key]
        [ForeignKey("DoctorVisit")]
        public int Id { get; set; }
        public string? DescriptionOfDestination { get; set; }


        public List<MedicalExamination>? MedicalExaminations { get; set; }
        public List<PhysicalTherapy>? PhysicalTherapys { get; set; }
        public List<PrescribedMedication>? PrescribedMedications { get; set; }
        public List<Analysis>? Analysis { get; set; }
        public int? TransferredDiseaseId { get; set; }
        public DoctorVisit DoctorVisit { get; set; }
    }
}
