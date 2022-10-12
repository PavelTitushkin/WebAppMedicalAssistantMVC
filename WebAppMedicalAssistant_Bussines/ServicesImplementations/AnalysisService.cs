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
using WebAppMedicalAssistant_DataBase.Entities;

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

        public async Task<List<MedicalInstitutionDto>> GetMedicalInstitutionsAsync()
        {
            try
            {
                var listMedicalInstitution = await _unitOfWork.MedicalInstitution
                    .Get()
                    .Select(entity => _mapper.Map<MedicalInstitutionDto>(entity))
                    .ToListAsync();

                return listMedicalInstitution;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> CreateAnalysisAsync(AnalysisDto analysisDto)
        {
            try
            {
                var analysis = _mapper.Map<Analysis>(analysisDto);
                if(analysis != null)
                {
                    await _unitOfWork.Analysis.AddEntityAsync(analysis);
                    var resultAdd = await _unitOfWork.Commit();
                    return resultAdd;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

    }
}
