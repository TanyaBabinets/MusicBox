namespace MusicBox.Models
{
	public class SortViewModel
	{
		public SortState NameSort { get; set; } 
		public SortState SongSort { get; set; }   
		public SortState GenreSort { get; set; }    
		
		public SortState Current { get; set; }     // значение свойства, выбранного для сортировки

		public SortViewModel(SortState sortOrder)
		{
			// значения по умолчанию
			NameSort = SortState.NameAsc;
			SongSort = SortState.SongAsc;
			
			NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
			SongSort = sortOrder == SortState.SongAsc ? SortState.SongDesc : SortState.SongAsc;
			
			Current = sortOrder;
		}
	}
}


