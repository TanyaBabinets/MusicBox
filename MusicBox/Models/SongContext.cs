using Microsoft.EntityFrameworkCore;

namespace MusicBox.Models
{

    public class SongContext : DbContext
    {
        public DbSet<Users> users { get; set; }
        public DbSet<Songs> songs { get; set; }
        public DbSet<Genres> genres { get; set; }

        public SongContext(DbContextOptions<SongContext> options)
            : base(options)

        {
            if (Database.EnsureCreated())
            {
                Genres r = new Genres { name = "Рок-музыка" };
                Genres p = new Genres { name = "Поп-музыка" };
				Genres l = new Genres { name = "Лаунж" };

				songs?.Add(new Songs
                {
                    name = "Не отпускай",
                    singer = "Земфира",
                    runtime = "4:05",
                    size = 9.31,
                    file = "/mp3/dont_let.mp3",
                    pic = "/img/zem.jpg",
                    Datetime = DateTime.Now,

                    genre = r
                });
                songs?.Add(new Songs
                {
                    name = "Остаться в живых",
                    singer = "Би-2",
                    runtime = "3:51",
                    size = 8.84,
                    file = "/mp3/last_hero.mp3",
                    pic = "/img/bi2.jpg",
                    Datetime = DateTime.Now,

                    genre = r
                });
                songs?.Add(new Songs
                {
                    name = "La isla Bonita",
                    singer = "Madonna",
                    runtime = "3:48",
                    size = 8.72,
                    file = "/mp3/Bonita.mp3",
                    pic = "/img/mad.jfif",
                    Datetime = DateTime.Now,

                    genre = p
                });
				songs?.Add(new Songs
				{
					name = "Hounted",
					singer = "Tailor Swift",
					runtime = "3:38",
					size = 9.3,
					file = "/mp3/Hounted.mp3",
					pic = "/img/swift.jpg",
					Datetime = DateTime.Now,

					genre = p
				});
				songs?.Add(new Songs
                {
                    name = "Masterpiece",
                    singer = "Madonna",
                    runtime = "4:01",
                    size = 5.52,
                    file = "/mp3/Masterpiece.mp3",
                    pic = "/img/mad.jfif",
                    Datetime = DateTime.Now,

                    genre = p
                });
                songs?.Add(new Songs
                {
                    name = "It must have been Love",
                    singer = "Roxette",
                    runtime = "4:01",
                    size = 5.52,
                    file = "/mp3/Must.mp3",
                    pic = "/img/roxx.jpg",
                    Datetime = DateTime.Now,

                    genre = p
                });
                songs?.Add(new Songs
                {
                    name = "Die Another Day",
                    singer = "Madonna",
                    runtime = "4:31",
                    size = 8.27,
                    file = "/mp3/Die_Another_Day.mp3",
                    pic = "/img/mad.jfif",
                    Datetime = DateTime.Now,

                    genre = p
                });
                songs?.Add(new Songs
                {
                    name = "Eye Of The Needle",
                    singer = "Sia",
                    runtime = "4:01",
                    size = 5.52,
                    file = "/mp3/Eye Of The Needle.mp3",
                    pic = "/img/sia.jpg",
                    Datetime = DateTime.Now,

                    genre = p
                });
				songs?.Add(new Songs
				{
					name = "Weekend",
					singer = "Fox",
					runtime = "4:01",
					size = 5.52,
					file = "/mp3/fox.mp3",
					pic = "/img/fox.jpg",
					Datetime = DateTime.Now,

					genre = l
				});
				songs?.Add(new Songs
                {
                    name = "Chandelier (Piano Version)",
                    singer = "Sia",
                    runtime = "4:01",
                    size = 4.59,
                    file = "/mp3/Chandelier.mp3",
                    pic = "/img/sia.jpg",
                    Datetime = DateTime.Now,

                    genre = p
                });

                SaveChanges();
            }
        }
    }
}
        
		



