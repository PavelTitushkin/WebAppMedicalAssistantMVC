using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_DataBase;

namespace WebAppMedicalAssistant_Bussines.ServicesImplementations
{
    public class FluorographyService : IFluorographyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FluorographyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        public async Task<List<FluorographyDto>> GetAllFluorographiesAsync(int userId) 
        {
            try
            {
                var listFlyorographies = await _unitOfWork.Fluorography
                    .Get().Where(entity => entity.UserId.Equals(userId))
                    //.FindBy(entity=>entity.UserId.Equals(userId))
                    .Include(dto => dto.MedicalInstitution)
                    .Select(flyoragraphies => _mapper.Map<FluorographyDto>(flyoragraphies))
                    .ToListAsync();

                
                return listFlyorographies;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
