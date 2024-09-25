namespace MusicBox.Models
{
	public class IndexViewModel
	{
		public IEnumerable<Songs> Songs { get; set; }
		public PageViewModel PageViewModel { get; }
		public FilterViewModel FilterViewModel { get; }
		public SortViewModel SortViewModel { get; }

		public IndexViewModel(IEnumerable<Songs> songs, PageViewModel pageViewModel,
			FilterViewModel filterViewModel, SortViewModel sortViewModel)
		{
			Songs = songs;
			PageViewModel = pageViewModel;
			FilterViewModel = filterViewModel;
			SortViewModel = sortViewModel;
		}
	}
}

