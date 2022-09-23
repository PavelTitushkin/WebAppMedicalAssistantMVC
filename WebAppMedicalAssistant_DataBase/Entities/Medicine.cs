﻿namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Medicine : IBaseEntity
    {
        public int Id { get; set; }
        public string NameOfMedicine { get; set; }
        public string ShortDescriptionOfMedicine { get; set; }

        public virtual PrescribedMedication PrescribedMedication { get; set; }
        public int PrescribedMedicationId { get; set; }
    }
}
