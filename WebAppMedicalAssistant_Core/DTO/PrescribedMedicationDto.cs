using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppMedicalAssistant_Core.DTO
{
    public class PrescribedMedicationDto
    {
        public int Id { get; set; }
        public DateTime StartDateOfMedication { get; set; }
        public DateTime EndDateOfMedication { get; set; }
        public string MedicineDose { get; set; }
        public decimal MedicinePrice { get; set; }

        public List<MedicineDto> MedicinesDto { get; set; }
        public UserDto UserDto { get; set; }
        public TransferredDiseaseDto TransferredDiseaseDto { get; set; }
        public AppointmentDto AppointmentDto { get; set; }
    }
}
