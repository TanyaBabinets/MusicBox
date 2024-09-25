using Microsoft.EntityFrameworkCore;
using MusicBox.Models;
using MusicBox.Repository;

namespace MusicBox.Repository
{
    public class SongRepository : IRepository<Songs>
    {
        private readonly SongContext _context;

        public SongRepository(SongContext context)
        {
            _context = context;
        }
		public async Task<List<Songs>> ToListAsync()
		{
			var song = await _context.songs.ToListAsync();
			return song;
		}
		public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

       
        public async Task Create(Songs item)
        {            
           _context.songs.Add(item);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var sDel = await _context.songs.FindAsync(id);

            if (sDel != null)
            {
                _context.songs.Remove(sDel);
                await _context.SaveChangesAsync();
            }
            else
            {              
                throw new Exception("Не получилось удалить");
            }
        }

        public async Task<Songs> GetById(int id)
        {
            var song = await _context.songs.Include(u => u.user).Include(g => g.genre).Where(s => s.Id == id).FirstOrDefaultAsync();
            return song;
        }

        public async Task<Songs> GetByName(string n)
        {

            return await _context.songs.FirstOrDefaultAsync(s => s.name == n);
        }

        public async Task<IEnumerable<Songs>> ToList()
        {
            var song = await _context.songs.Include(m => m.genre).ToListAsync();
            return song;
        }
        
        public void Update(Songs item)
        {

            var Item = _context.songs.Find(item.Id);

            if (Item != null)
            {
                Item.name = item.name;
                Item.singer = item.singer;
                Item.runtime = item.runtime;
                Item.size = item.size;
                Item.file = item.file;
                Item.pic = item.pic;
                Item.Datetime = item.Datetime;
                Item.user = item.user;
                Item.genre = item.genre;

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Песня не найдена");
            }
        }    

        public Task<string?> UsersToApprove()
        {
            throw new NotImplementedException();
        }
		public IQueryable<Songs> GetFilteredSongs(string singer, int genre, SortState sortOrder)
		{
			var item =_context.songs.Include(s => s.genre).AsQueryable();

			// Фильтрация по исполнителю
			if (!string.IsNullOrEmpty(singer))
			{
				item = item.Where(s => s.singer.ToLower().Contains(singer.ToLower()));
			}

			// Фильтрация по жанру
			if (genre != 0)
			{
				item = item.Where(s => s.GenreId == genre);
			}

            // Сортировка
           
            item = sortOrder switch
            {
                SortState.SingerDesc => item.OrderByDescending(s => s.singer),//singer
			
				SortState.SongAsc => item.OrderBy(s => s.name),//song name
                SortState.SongDesc => item.OrderByDescending(s => s.name),

                _ => item.OrderBy(s => s.singer),
            };
            return item;
            //return await item.ToListAsync();
		}

		public IQueryable<Songs> Query()
		{
			return _context.songs; 
		}
	}
}

