using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Medicine : IBaseEntity
    {
        public int Id { get; set; }
        public string NameOfMedicine { get; set; }
        public string ReleaseForm { get; set; }
        public string DosageForm {get; set; }
        public string WayOfIntroduction { get; set; }
        public string LinkToInstructions { get; set; }

        public List<PrescribedMedication> PrescribedMedications { get; set; }
    }
}
