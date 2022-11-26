using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppMedicalAssistant_DataBase.Entities
{
    public class ScanOfAnalysisDocument : IBaseEntity
    {
        public int Id { get; set; }
        public byte[]? ScanOfAnalysis { get; set; }
        public Analysis? Analysis { get; set; }
        public int AnalysisId { get; set; }
    }
}
