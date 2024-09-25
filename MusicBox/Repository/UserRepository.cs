
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBox.Models;
using MusicBox.Repository;

namespace MusicBox.Repository
{
    public class UserRepository : IRepository<Users>
    {
        private readonly SongContext _context;

        public UserRepository(SongContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Users>> UsersToApprove()
        {
            return await _context.users.Where(u => !u.IsActivated).ToListAsync();
           // return _context.users.Where(u => !u.IsActivated);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task Create(Users item)
        {
            await _context.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            var uDel = await _context.users.FindAsync(id);

            if (uDel != null)
            {
                _context.users.Remove(uDel);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Не получилось удалить");
            }
        }

        public async Task<Users> GetById(int? id)
        {
           
                return await _context.users.FindAsync(id);           
        }




        //public async Task<IActionResult> GetById(int? id)///Details???
        //{
        //    if (id == null || await repo.ToList() == null)
        //    {
        //        return NotFound();
        //    }
        //    var user = await repo.GetById((int)id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}


        public async Task<Users> GetByName(string name)
        {
           
            return await _context.users.FirstOrDefaultAsync(u => u.Login == name);
        }




        //public async Task<IEnumerable<Users>> ToList()
        //{
        //    var user = await _context.ToListAsync();
        //    return user;
        //}
        //public async Task<List<Users>> ToListAsync()
        //{
        //	var users = await _context.users.ToListAsync();
        //	return users;
        //}
        public void Update(Users item)
        {
			_context.Entry(item).State = EntityState.Modified;
		}

        public async Task<Users> GetById(int id)
        {
            return await _context.users.FindAsync(id);
        }
		
		public async Task<IEnumerable<Users>> ToList()
		{
			
			var user = await _context.users.ToListAsync();
            return user;
        }

		public IQueryable<Users> Query()
		{
			throw new NotImplementedException();
		}

		IQueryable<Users> IRepository<Users>.GetFilteredSongs(string singer, int genre, SortState sortOrder)
		{
			throw new NotImplementedException();
		}


		//public async Task<IActionResult> GetById(int? id)///Details???
		//{
		//    if (id == null || await repo.ToList() == null)
		//    {
		//        return NotFound();
		//    }
		//    var user = await repo.GetById((int)id);

		//    if (user == null)
		//    {
		//        return NotFound();
		//    }

		//    return View(user);
		//}



		//public async Task IRepository<Users>.Create(Users item)
		//{
		//    await _context.Users.AddAsync(item);
		//}



		//public async Task IRepository<Users>.Delete(int id)
		//{
		//    Users? c = await _context.Users.FindAsync(id);
		//    if (c != null)
		//        _context.Messages.Remove(c);
		//}
	}
}

