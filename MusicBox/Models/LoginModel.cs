using System.ComponentModel.DataAnnotations;

namespace MusicBox.Models
{

    public class LoginModel
    {

		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
			   ErrorMessageResourceName = "LoginRequired")]
		[Display(Name = "Login", ResourceType = typeof(Resources.Resource))]

		//[Required(ErrorMessage = "Логин или пароль неверный")]
  //      [Display(Name = "Введите логин: ")]
        public string? Login { get; set; }

		//[Required(ErrorMessage = "Пароль или логин неверный")]
		//[Display(Name = "Введите пароль: ")]
		[DataType(DataType.Password)]

		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
			   ErrorMessageResourceName = "PasswordRequired")]
		[Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
		public string? Password { get; set; }
    }
}


