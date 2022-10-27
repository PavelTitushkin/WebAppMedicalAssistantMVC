using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebAppMedicalAssistant_Core
{
    public enum FormOfTransferredDisease
    {
        [Display(Name = "Лёгкая")]
        Easy,
        [Display(Name = "Средне лёгкая")]
        MediumLight,
        [Display(Name = "Средняя")]
        Medium,
        [Display(Name = "Средне тяжёлая")]
        MediumHeavy,
        [Display(Name = "Тяжёлая")]
        Heavy
    }
}
