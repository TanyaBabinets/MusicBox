using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicBox.Models
{
	public class FilterViewModel
	{
		public FilterViewModel(IEnumerable<Genres> genres, int genre, string singer)
		{
			var genresList = genres?.ToList() ?? new List<Genres>();//проверка на null
			genresList.Insert(0, new Genres { name = "Все жанры", Id = 0 });
			//	Genres = new SelectList(genresList, "Id", "Name", genre);// выдает ошибку в чтении свойств списка
			Genres = new SelectList(genresList.Select(g => new SelectListItem
			{
				Value = g.Id.ToString(),
				Text = g.name
			}), "Value", "Text", genre);
			SelectedGenre = genre;
			SelectedSinger = singer;
		}

		public SelectList Genres { get; } // список жанров
		public int SelectedGenre { get; } // выбранный жанр
		public string SelectedSinger { get; } // выбранный певец
	}
}
