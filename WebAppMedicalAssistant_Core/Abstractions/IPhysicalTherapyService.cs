using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core.DTO;

namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IPhysicalTherapyService
    {
        Task<int> CreatePhysicalTherapyAsync(PhysicalTherapyDto dto);
    }
}
