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

        public async Task<List<TransferredDiseaseDto>> GetAllTransferredDiseaseAsync()
        {
            try
            {
                var listTransferredDiseaseis = await _unitOfWork.TransferredDisease.Get()
                    .Include(dto=>dto.Diseases)
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
