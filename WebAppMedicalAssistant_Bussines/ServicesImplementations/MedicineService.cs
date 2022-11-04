using AutoMapper;
using AutoMapper.Configuration.Annotations;
using HtmlAgilityPack;
using Microsoft.VisualBasic;
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
                var nodes = htmlDoc.DocumentNode.Descendants(0).Where(node => node.HasClass("tooltip-info-header"));
                
                var nodeList = new List<string>();
                var nodeValue = "https://tabletka.by";
                foreach (var node in nodes)
                {
                    var tempNodes = node.ChildNodes
                        .Select(node => node.GetAttributes("href"));
                    foreach (var tempNode in tempNodes)
                    {
                        foreach (var item in tempNode)
                        {
                            if (item != null)
                            {
                                if (item.Value.Contains("result"))
                                {
                                    nodeValue += item.Value;
                                    nodeList.Add(nodeValue);
                                    nodeValue = "https://tabletka.by";
                                }
                            }
                        }
                    }
                }
                var nodeListDistinct = nodeList.Distinct();

                //foreach (var node in nodes)
                //{
                //    var tempNode = node.ChildNodes
                //        .Where(node => node.Name.Equals("a"))
                //        .Select(node => node.OuterHtml)
                //        .Aggregate((i, j) => i + Environment.NewLine + j);
                //    nodeList.Add(tempNode);
                //}
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
