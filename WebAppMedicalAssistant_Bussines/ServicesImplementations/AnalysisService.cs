using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppMedicalAssistant_Core;
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
                    .Include(include=> include.ScanOfAnalysisDocument)
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
                analysis.Id = 0;
                await _unitOfWork.Analysis.AddEntityAsync(analysis);
                var resultAdd = await _unitOfWork.Commit();
                var id = analysis.Id;
                return id;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<int> CreateScanOfDocumentsAnalysisAsync(ScanOfAnalysisDocumentDto dto)
        {
            try
            {
                var entity = _mapper.Map<ScanOfAnalysisDocument>(dto);
                await _unitOfWork.ScanOfAnalysisDocument.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task<int> UpdateAnalysisAsync(AnalysisDto dto, int id)
        {
            try
            {
                var sourceDto = await GetAnalysisByIdAsync(id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if (dto.NameOfAnalysis != sourceDto.NameOfAnalysis)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.NameOfAnalysis),
                            PropertyValue = dto.NameOfAnalysis
                        });
                    }
                    if (dto.DateOfAnalysis != sourceDto.DateOfAnalysis)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.DateOfAnalysis),
                            PropertyValue = dto.DateOfAnalysis
                        });
                    }
                    if (dto.MedicalInstitutionId != sourceDto.MedicalInstitutionId)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.MedicalInstitutionId),
                            PropertyValue = dto.MedicalInstitutionId
                        });
                    }
                }

                await _unitOfWork.Analysis.PatchAsync(id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAnalysisAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.Analysis
                    .FindBy(entity => entity.Id == id)
                    .FirstOrDefaultAsync();
                _unitOfWork.Analysis.Remove(entity);

                await _unitOfWork.Commit();
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ScanOfAnalysisDocumentDto?> GetScanOfAnalysisByIdAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.ScanOfAnalysisDocument
                    .FindBy(entity => entity.Id == id)
                    .AsNoTracking()
                    .Select(entity => _mapper.Map<ScanOfAnalysisDocumentDto>(entity))
                    .FirstOrDefaultAsync();

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateScanOfDocumentsAnalysisAsync(ScanOfAnalysisDocumentDto dto)
        {
            try
            {
                var sourceDto = await GetScanOfAnalysisByIdAsync(dto.Id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if (dto.ScanOfAnalysis != sourceDto.ScanOfAnalysis)
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.ScanOfAnalysis),
                            PropertyValue = dto.ScanOfAnalysis
                        });
                    }
                }

                await _unitOfWork.ScanOfAnalysisDocument.PatchAsync(dto.Id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task DeleteScanOfAnalysisAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.ScanOfAnalysisDocument
                    .FindBy(entity => entity.Id == id)
                    .FirstOrDefaultAsync();
                _unitOfWork.ScanOfAnalysisDocument.Remove(entity);

                await _unitOfWork.Commit();
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
