using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class AnalysisService : IAnalysisService
    {
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AnalysisService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AnalysisDto>> GetAllAnalysisAsync(int userId)
        {
            try
            {
                var listAnalysis = await _unitOfWork.Analysis
                    .FindBy(entity => entity.UserId.Equals(userId))
               .Include(include => include.MedicalInstitution)
               .Select(analysis => _mapper.Map<AnalysisDto>(analysis))
               .ToListAsync();

                return listAnalysis;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
