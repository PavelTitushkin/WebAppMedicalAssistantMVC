﻿namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class Analysis: IBaseEntity
    {
        public int Id { get; set; }
        public string NameOfAnalysis { get; set; }
        public DateTime DateOfAnalysis { get; set; }
        public byte[]? ScanOfAnalysisDocument { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        public MedicalInstitution MedicalInstitution { get; set; }
        public int MedicalInstitutionId { get; set; }
    }
}
