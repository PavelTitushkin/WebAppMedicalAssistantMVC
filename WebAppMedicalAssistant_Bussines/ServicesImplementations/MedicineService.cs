using AutoMapper;
using HtmlAgilityPack;
using WebAppMedicalAssistant_Core.Abstractions;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.Abstractions;

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

        public void AddMedicineAsync(string nameOfMedicine)
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
                var listDto = (await _unitOfWork.Medicine.GetAllAsync()).Select(entity => _mapper.Map<MedicineDto>(entity)).ToList();

                return listDto;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
