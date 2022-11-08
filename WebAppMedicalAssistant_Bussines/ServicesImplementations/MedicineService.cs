using AutoMapper;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAppMedicalAssistant_Core;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;
using WebAppMedicalAssistant_DataBase.Entities;

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

        public List<string>? SearchMedicineInTabletkaByAsync(string nameOfMedicine)
        {
            try
            {
                var urlSearchSite = $"https://tabletka.by/search?request={nameOfMedicine}";

                var web = new HtmlWeb();
                var htmlDoc = web.Load(urlSearchSite);

                var table = htmlDoc.DocumentNode.Descendants(0)
                    .Where(node => node.Id == "base-select")
                    .FirstOrDefault();

                var links = table?.Descendants()
                    .Where(x => x.Name == "a" && x.GetAttributeValue("href", string.Empty).StartsWith("/result"))
                    .Select(x => x.GetAttributeValue("href", string.Empty))
                    .Distinct()
                    .ToList();

                return links;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddMedicine(List<string>? listLinkMedicine)
        {
            try
            {
                var listMedicineDto = new List<MedicineDto>();
                var urlTabletkaBy = $"https://tabletka.by";
                var web = new HtmlWeb();
                foreach (var link in listLinkMedicine)
                {
                    var htmlDoc = web.Load(urlTabletkaBy + link);

                    var nameMedicine = htmlDoc.DocumentNode.Descendants()
                        .Where(x => x.Name == "h1")
                        .Select(x => x.FirstChild.InnerText)
                        .FirstOrDefault()?
                        .Replace(Environment.NewLine, string.Empty);

                    var linkToInstruction = htmlDoc.DocumentNode.Descendants()
                        .Where(x => x.Name == "a" && x.GetAttributeValue("href", string.Empty).StartsWith("/instructions"))
                        .Select(x => x.GetAttributeValue("href", string.Empty))
                        .FirstOrDefault();

                    var medicineDto = new MedicineDto()
                    {
                        NameOfMedicine = nameMedicine,
                        LinkToInstructions = urlTabletkaBy + linkToInstruction
                    };

                    listMedicineDto.Add(medicineDto);
                }

                var existsMedicines = await _unitOfWork.Medicine.Get()
                    .Select(medicine => medicine.NameOfMedicine)
                    .Distinct()
                    .ToArrayAsync();

                var entities = listMedicineDto.Where(dto => !existsMedicines.Contains(dto.NameOfMedicine))
                    .Select(dto => _mapper.Map<Medicine>(dto)).ToList();

                await _unitOfWork.Medicine.AddRangeAsync(entities);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<MedicineDto>> GetAllMedicineAsync()
        {
            try
            {
                var listDto = (await _unitOfWork.Medicine.GetAllAsync())
                    .Select(entity => _mapper.Map<MedicineDto>(entity))
                    .ToList();

                return listDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MedicineDto> GetByIdMedicineAsync(int id)
        {
            try
            {
                var dto = await _unitOfWork.Medicine
                    .FindBy(entity => entity.Id.Equals(id))
                    .Select(entity => _mapper.Map<MedicineDto>(entity))
                    .FirstOrDefaultAsync();
                        
                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateMedicineAsync(MedicineDto dto, int id)
        {
            try
            {
                var sourceDto = await GetByIdMedicineAsync(id);
                var patchList = new List<PatchModel>();
                if (dto != null)
                {
                    if (!dto.NameOfMedicine.Equals(sourceDto.NameOfMedicine))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.NameOfMedicine),
                            PropertyValue = dto.NameOfMedicine
                        });
                    }
                    if (!dto.LinkToInstructions.Equals(sourceDto.LinkToInstructions))
                    {
                        patchList.Add(new PatchModel()
                        {
                            PropertyName = nameof(dto.LinkToInstructions),
                            PropertyValue = dto.LinkToInstructions
                        });
                    }
                }

                await _unitOfWork.Medicine.PatchAsync(id, patchList);

                return await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteMedicineAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.Medicine
                    .FindBy(entity => entity.Id.Equals(id))
                    .Include(include => include.PrescribedMedications)
                    .FirstOrDefaultAsync();

                _unitOfWork.Medicine.Remove(entity);
                await _unitOfWork.Commit();
            }

            catch (Exception)
            {
                throw new ArgumentException("", nameof(id));
            }
        }
    }
}
