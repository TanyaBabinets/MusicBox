using MusicBox.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicBox.Models
{
    public class Songs
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Название")]
        public string? name { get; set; }
        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Исполнитель")]
        public string? singer { get; set; }
        [Display(Name = "Длительность")]
        public string? runtime { get; set; }
        [Display(Name = "Размер файла в МБ")]
        public double? size { get; set; }
        [Display(Name = "Музыкальный файл")]
        //[Required(ErrorMessage = "Загрузите файл в формате мр3 ")]
        public string? file { get; set; }
        //[Required(ErrorMessage = "Загрузите файл в формате .jpg ")]
        public string? pic { get; set; }
        [Display(Name = "Дата загрузки")]
        public DateTime? Datetime { get; set; }
        [Display(Name = "Загружено пользователем:")]
        public Users? user { get; set; }
        [Display(Name = "Жанр")]
        
        public Genres? genre { get; set; }
		
	}
}
