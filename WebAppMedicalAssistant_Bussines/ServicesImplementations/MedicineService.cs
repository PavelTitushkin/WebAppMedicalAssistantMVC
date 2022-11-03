using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;

namespace WebAppMedicalAssistant_Bussines.ServicesImplementations
{
    public class MedicineService : IMedicineService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MedicineService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MedicineDto>> GetAllMedicineAsync()
        {
            try
            {
                var listDto = (await _unitOfWork.Medicine.GetAllAsync()).Select(entity => _mapper.Map<MedicineDto>(entity)).ToList();

                return listDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
