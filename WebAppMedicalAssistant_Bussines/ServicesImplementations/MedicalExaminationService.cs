﻿using AutoMapper;
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
    public class MedicalExaminationService : IMedicalExaminationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MedicalExaminationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MedicalExaminationDto>> GetAllMedicalExaminationAsync()
        {
            try
            {
                var listMedicalExaminations = await _unitOfWork.MedicalExamination.Get()
                    .Include(dto=>dto.Appointment)
                    .Select(medicalExamination=>_mapper.Map<MedicalExaminationDto>(medicalExamination))
                    .ToListAsync();
                return listMedicalExaminations;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
