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
                 Genres s = new Genres { name = "Саундтреки" };


				songs?.Add(new Songs
				{
					name = "Vitamin D",
					singer = "Monatik",
					runtime = "03:29",
					size = 8.0,
					file = "/mp3/Vitamin D.mp3",
					pic = "/img/monatik.jfif",
					Datetime = DateTime.Now,

					genre = p
				});
				songs?.Add(new Songs
				{
					name = "Sad Girl",
					singer = "Lana Del Rey",
					runtime = "05:18",
					size = 4.9,
					file = "/mp3/Sad Girl.mp3",
					pic = "/img/lana.jfif",
					Datetime = DateTime.Now,

					genre = l
				});
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
					name = "My Heart Will Go On",
					singer = "Celine Dion",
					runtime = "4:40",
					size = 10.7,
					file = "/mp3/My Heart Will Go On.mp3",
					pic = "/img/dion.jpg",
					Datetime = DateTime.Now,

					genre = s
				});
				songs?.Add(new Songs
				{
					name = "Time",
					singer = "Hans Zimmerman",
					runtime = "04:36",
					size = 10.5,
					file = "/mp3/Time.mp3",
					pic = "/img/zimmer.jpg",
					Datetime = DateTime.Now,

					genre = s
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
					name = "Interstellar",
					singer = "Hans Zimmerman",
					runtime = "06:53",
					size = 15.8,
					file = "/mp3/Interstellar.mp3",
					pic = "/img/zimmer.jpg",
					Datetime = DateTime.Now,

					genre = s
				});
				songs?.Add(new Songs
				{
					name = "Summertime Sadness",
					singer = "Lana Del Rey",
					runtime = "04:25",
					size = 6.1,
					file = "/mp3/Summertime Sadness.mp3",
					pic = "/img/lana.jfif",
					Datetime = DateTime.Now,

					genre = l
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
					name = "Старі Фотографії",
					singer = "Monatik",
					runtime = "02:38",
					size = 6.2,
					file = "/mp3/oldFotos.mp3",
					pic = "/img/monatik.jfif",
					Datetime = DateTime.Now,

					genre = p
				});
				songs?.Add(new Songs
                {
                    name = "Haunted",
                    singer = "Tailor Swift",
                    runtime = "3:38",
                    size = 9.3,
                    file = "/mp3/Haunted.mp3",
                    pic = "/img/swift.jpg",
                    Datetime = DateTime.Now,

                    genre = p
                });
				songs?.Add(new Songs
				{
					name = "Davy Jones (Пираты Карибского Моря)",
					singer = "Hans Zimmerman",
					runtime = "03:15",
					size = 6.0,
					file = "/mp3/Pirates of Carribean.mp3",
					pic = "/img/zimmer.jpg",
					Datetime = DateTime.Now,

					genre = s
				});
				songs?.Add(new Songs
				{
					name = "Young and Beautiful",
					singer = "Lana Del Rey",
					runtime = "03:55",
					size = 9.0,
					file = "/mp3/Young and Beautiful.mp3",
					pic = "/img/lana.jfif",
					Datetime = DateTime.Now,

					genre = s
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
					name = "West Coast",
					singer = "Lana Del Rey",
					runtime = "04:12",
					size = 5.8,
					file = "/mp3/West Coast.mp3",
					pic = "/img/lana.jfif",
					Datetime = DateTime.Now,

					genre = l
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
					name = "In The End",
					singer = "Linkin Park",
					runtime = "03:36",
					size = 8.3,
					file = "/mp3/In The End.mp3",
					pic = "/img/park.jfif",
					Datetime = DateTime.Now,

					genre = r
				});
				songs?.Add(new Songs
				{
					name = "Faint",
					singer = "Linkin Park",
					runtime = "02:42",
					size = 6.4,
					file = "/mp3/Faint.mp3",
					pic = "/img/park.jfif",
					Datetime = DateTime.Now,

					genre = r
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
				songs?.Add(new Songs
				{
					name = "Love it. Ритм",
					singer = "Monatik",
					runtime = "03:55",
					size = 9.0,
					file = "/mp3/Love it.mp3",
					pic = "/img/monatik.jfif",
					Datetime = DateTime.Now,

					genre = p
				});
				SaveChanges();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи "один ко многим" между Genre и Songs
            modelBuilder.Entity<Songs>()
                .HasOne(s => s.genre)
                .WithMany()
                .HasForeignKey(s => s.GenreId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

        
		



