using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace WebAppMedicalAssistantMVC.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [EmailAddress(ErrorMessage = "Не коректно указан электронный адрес")]
        [Remote("CheckEmail", 
            "Account", 
            HttpMethod = WebRequestMethods.Http.Post, 
            ErrorMessage = "Пользователь с таким Email уже зарегистрирован")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [Compare(nameof(Password), ErrorMessage =("Пароль не совпадает"))]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}
