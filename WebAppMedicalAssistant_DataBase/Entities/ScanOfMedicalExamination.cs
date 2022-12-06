using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class ScanOfMedicalExamination: IBaseEntity
    {   
        public int Id { get; set; }
        public byte[]? ScanData { get; set; }
        public int MedicalExaminationId { get; set; }
        public MedicalExamination? MedicalExamination { get; set; }
    }
}
