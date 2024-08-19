

using System.ComponentModel.DataAnnotations;

namespace MusicBox.Models
{
    public class RegisterModel
    {
		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
						ErrorMessageResourceName = "NameURequired")]
		[Display(Name = "NameU", ResourceType = typeof(Resources.Resource))]
		//[Required(ErrorMessage = "Введите имя")]
  //      [Display(Name = "Имя пользователя: ")]
        public string? FirstName { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
				ErrorMessageResourceName = "SurnameRequired")]
		[Display(Name = "Surname", ResourceType = typeof(Resources.Resource))]
		//[Required(ErrorMessage = "Введите фамилию")]
  //      [Display(Name = "Фамилия пользователя: ")]
        public string? LastName { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
			   ErrorMessageResourceName = "LoginRequired")]
		[Display(Name = "Login", ResourceType = typeof(Resources.Resource))]

		//[Required(ErrorMessage = "Такой логин уже существует")]
  //      [Display(Name = "Придумайте логин: ")]
        public string? Login { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
		   ErrorMessageResourceName = "EmailRequired")]
		[Display(Name = "Email", ResourceType = typeof(Resources.Resource))]

		//[Required(ErrorMessage = "Введите email")]
  //      [Display(Name = "Email: ")]
        public string? Email { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
			   ErrorMessageResourceName = "PasswordRequired")]
		[Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
		//[Required(ErrorMessage = "Введите пароль")]
		//      [Display(Name = "Пароль: ")]
		[DataType(DataType.Password)]
        public string? Password { get; set; }


    
		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
			   ErrorMessageResourceName = "PasswordConfirm")]
		[Display(Name = "PasswordC", ResourceType = typeof(Resources.Resource))]
		//[Display(Name = "Подтвердите пароль: ")]
  //      [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}

