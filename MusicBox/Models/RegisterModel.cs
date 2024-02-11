using System.ComponentModel.DataAnnotations;

namespace MusicBox.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя пользователя: ")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия пользователя: ")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Такой логин уже существует")]
        [Display(Name = "Придумайте логин: ")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [Display(Name = "Email: ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль: ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "Подтвердите пароль: ")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}

