using Microsoft.EntityFrameworkCore;
using MusicBox.Models;


namespace MusicBox.Repository
{
    public class GenreRepository : IRepository<Genres>
    {
        private readonly SongContext _context;

        public GenreRepository(SongContext context)
        {
            _context = context;
        }

		public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task Create(Genres item)
        {
            _context.genres.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var gDel = await _context.genres.FindAsync(id);

            if (gDel != null)
            {
                _context.genres.Remove(gDel);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Не получилось удалить");
            }
        }

        public async Task<Genres> GetById(int id)
        {
            return await _context.genres.FindAsync(id);
        }

        public async Task<Genres> GetByName(string n)
        {

            return await _context.genres.FirstOrDefaultAsync(s => s.name == n);
        }

        public async Task<IEnumerable<Genres>> ToList()
        {
            var genres = await _context.genres.ToListAsync();
            return genres;
        }
        public void Update(Genres item)
        {
            
            var Item = _context.genres.Find(item.Id);

            if (Item != null)
            {
             Item.name = item.name;
              
                _context.SaveChanges();
            }
            else
            {              
                throw new Exception("Жанр не найден"); 
            }
        }

        public Task<string?> UsersToApprove()
        {
            throw new NotImplementedException();
        }
    }
}

