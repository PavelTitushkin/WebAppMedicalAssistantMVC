using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppMedicalAssistant_Core.DTO
{
    public class ScanOfAnalysisDocumentDto
    {
        public int Id { get; set; }
        public byte[]? ScanOfAnalysis { get; set; }
        public int AnalysisId { get; set; }
    }
}
