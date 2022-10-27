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
    public class TransferredDiseaseService : ITransferredDiseaseService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransferredDiseaseService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateDiseaseAsync(DiseaseDto dto)
        {
            try
            {
                var entity = _mapper.Map<Disease>(dto);
                await _unitOfWork.Disease.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateTransferredDiseaseAsync(TransferredDiseaseDto dto)
        {
            try
            {
                var entity = _mapper.Map<TransferredDisease>(dto);
                await _unitOfWork.TransferredDisease.AddEntityAsync(entity);
                var result = await _unitOfWork.Commit();
                var lastId = entity.Id;

                return lastId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<DiseaseDto>> GetAllDiseaseAsync()
        {
            try
            {
                var dto = await _unitOfWork.Disease.Get()
                    .Select(entity => _mapper.Map<DiseaseDto>(entity))
                    .ToListAsync();

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TransferredDiseaseDto>> GetAllTransferredDiseaseAsync(int id)
        {
            try
            {
                var listTransferredDiseaseis = await _unitOfWork.TransferredDisease
                    .FindBy(entity => entity.UserId.Equals(id))
                    .Include(include => include.Disease)
                    .Select(transferredDisease => _mapper.Map<TransferredDiseaseDto>(transferredDisease))
                    .ToListAsync();
                return listTransferredDiseaseis;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
