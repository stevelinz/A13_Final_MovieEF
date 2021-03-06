using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieLibraryOO.DataModels;
namespace MovieLibraryOO.Context
{
    public interface IMovieContext
    {
        DbSet<Genre> Genres { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<MovieGenre> MovieGenres { get; set; }
        DbSet<Occupation> Occupations { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserMovie> UserMovies { get; set; }
    }

    public class MovieContext : DbContext, IMovieContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var config = new ConfigurationBuilder()
            // .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            // .Build();

            try
            {
                string user = System.IO.File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "user.cnn"));
                string pass = System.IO.File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "pass.cnn"));


                optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer(@"Server=bitsql.wctc.edu; Database=A_13_SL_22097; User Id=" + user + "; Password=" + pass + ";");

                // IMPORTANT NEED THIS LINE WHEN YOU RUN THE MIGRATION (it can't guess the login/pw) 
                // the ONLY line uncommented should be the updated line below 
                //  optionsBuilder.UseSqlServer(@"Server=bitsql.wctc.edu; Database=A_13_SL_22097; User Id=*****; Password=****;");

            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Something went wrong this time in Context.");
            }
        }

    }

}