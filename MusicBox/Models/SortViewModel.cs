namespace MusicBox.Models
{
	public class SortViewModel
	{
		public SortState SingerSort { get; set; } 
		public SortState SongSort { get; set; }   
		public SortState GenreSort { get; set; }    
		
		public SortState Current { get; set; }     // значение свойства, выбранного для сортировки

		public SortViewModel(SortState sortOrder)
		{
			// значения по умолчанию
			SingerSort = SortState.SingerAsc;
			SongSort = SortState.SongAsc;
			
			SingerSort = sortOrder == SortState.SingerAsc ? SortState.SingerDesc : SortState.SingerAsc;
			SongSort = sortOrder == SortState.SongAsc ? SortState.SongDesc : SortState.SongAsc;
			
			Current = sortOrder;
		}
	}
}


