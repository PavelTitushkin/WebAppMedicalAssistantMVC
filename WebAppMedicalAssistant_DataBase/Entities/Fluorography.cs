namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Fluorography : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime DataOfExamination { get; set; }
        public DateTime EndDateOfSurvey { get; set; }
        public string NumberFluorography { get; set; }
        public bool Result { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual MedicalInstitution? MedicalInstitution { get; set; }
        public int? MedicalInstitutionId { get; set; }
    }
}
