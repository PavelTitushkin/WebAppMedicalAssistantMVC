using System.ComponentModel.DataAnnotations;

namespace WebAppMedicalAssistantMVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Не указан электронный адрес")]
        [EmailAddress(ErrorMessage = "Не коректно указан электронный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
