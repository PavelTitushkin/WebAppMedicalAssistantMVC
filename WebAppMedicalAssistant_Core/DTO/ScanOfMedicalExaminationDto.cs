using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppMedicalAssistant_Core.DTO
{
    public class ScanOfMedicalExaminationDto
    {
        public int Id { get; set; }
        public byte[]? ScanData { get; set; }
        public int MedicalExaminationId { get; set; }
        public MedicalExaminationDto? MedicalExamination { get; set; }
    }
}
