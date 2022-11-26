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

        public async Task<AnalysisDto> GetAnalysisByIdAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.Analysis
                    .FindBy(entity => entity.Id==id)
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitution)
                    .Select(analysis => _mapper.Map<AnalysisDto>(analysis))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IOrderedQueryable<AnalysisDto>> GetAllAnalysisAsync(int userId)
        {
            try
            {
                var listAnalysis = await _unitOfWork.Analysis
                    .FindBy(entity => entity.UserId.Equals(userId))
                    .AsNoTracking()
                    .Include(include => include.MedicalInstitution)
                    .Select(analysis => _mapper.Map<AnalysisDto>(analysis))
                    .ToListAsync();
                var queryList = listAnalysis.AsQueryable().OrderBy(x => x.DateOfAnalysis);

                return queryList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<AnalysisDto>> GetPeriodAnalysisAsync(DateTime SearchDateStart, DateTime SearchDateEnd, int userId)
        {
            try
            {
                var listAnalysis = await _unitOfWork.Analysis
                    .FindBy(entity => entity.UserId.Equals(userId))
                    .Where(entityData => entityData.DateOfAnalysis >= SearchDateStart && entityData.DateOfAnalysis <= SearchDateEnd)
                    .Include(include => include.MedicalInstitution)
                    .OrderBy(entity => entity.DateOfAnalysis)
                    .Select(analysis => _mapper.Map<AnalysisDto>(analysis))
                    .ToListAsync();

                return listAnalysis;
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
                await _unitOfWork.Analysis.AddEntityAsync(analysis);
                var resultAdd = await _unitOfWork.Commit();

                return resultAdd;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

    }
}
