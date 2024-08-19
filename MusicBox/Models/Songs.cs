using Humanizer.Localisation;
using MusicBox.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBox.Models
{
    public class Songs
    {
        public int Id { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
				ErrorMessageResourceName = "NameRequired")]
		[Display(Name = "Name", ResourceType = typeof(Resources.Resource))]

		//[Required(ErrorMessage = "Заполните поле")]
  //      [Display(Name = "Название")]
        public string? name { get; set; }

		//[Required(ErrorMessage = "Заполните поле")]
		//[Display(Name = "Исполнитель")]
		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
				ErrorMessageResourceName = "SingerRequired")]
		[Display(Name = "Singer", ResourceType = typeof(Resources.Resource))]
		public string? singer { get; set; }

	
		[Display(Name = "Runtime", ResourceType = typeof(Resources.Resource))]
		//[Display(Name = "Длительность")]
        public string? runtime { get; set; }

		[Display(Name = "Runtime", ResourceType = typeof(Resources.Resource))]
		//[Display(Name = "Размер файла в МБ")]
        public double? size { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
				ErrorMessageResourceName = "file_mp3")]
		[Display(Name = "file", ResourceType = typeof(Resources.Resource))]
		//[Display(Name = "Музыкальный файл")]
        //[Required(ErrorMessage = "Загрузите файл в формате мр3 ")]
        public string? file { get; set; }
		//[Required(ErrorMessage = "Загрузите файл в формате .jpg ")]

		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
			ErrorMessageResourceName = "file_jpg")]
		[Display(Name = "pic", ResourceType = typeof(Resources.Resource))]
		public string? pic { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
			ErrorMessageResourceName = "chooseDate")]
		[Display(Name = "date", ResourceType = typeof(Resources.Resource))]

		//[Display(Name = "Дата загрузки")]
		public DateTime? Datetime { get; set; }
		//[Display(Name = "Дата загрузки")]
		//[NotMapped]
		//public string datetime { get; set; }


		[Display(Name = "LoadedBy", ResourceType = typeof(Resources.Resource))]

		//[Display(Name = "Загружено пользователем:")]
        public Users? user { get; set; }


		[Required(ErrorMessageResourceType = typeof(Resources.Resource),
			ErrorMessageResourceName = "chooseGenre")]
		[Display(Name = "Genre", ResourceType = typeof(Resources.Resource))]
		//[Display(Name = "Жанр")]
       
        public Genres? genre { get; set; }
        [NotMapped]
        public int UserId { get; set; }
        [NotMapped]
        public int GenreId { get; set; }

    }
}
