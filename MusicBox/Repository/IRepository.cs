using MusicBox.Models;
using System.Linq.Expressions;

namespace MusicBox.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ToList(); 
		Task<T> GetById(int id);
        Task<T> GetByName(string name);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
        Task Save();
		IQueryable<T> GetFilteredSongs(string singer, int genre, SortState sortOrder);
		// void Update(Task<Users> user);
		IQueryable<T> Query();
	}
}
//Task<List<T>> ToListAsync();
//Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
//Task RemoveAsync(T entity);
//Task SaveAsync();