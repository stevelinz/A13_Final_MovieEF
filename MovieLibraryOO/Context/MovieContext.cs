using System.IO;
using Microsoft.EntityFrameworkCore;
using MovieLibraryOO.DataModels;

namespace MovieLibraryOO.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }

       
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            try
            {
                string user = System.IO.File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "user.cnn"));
                string pass = System.IO.File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "pass.cnn"));

                optionsBuilder.UseSqlServer(@"Server=bitsql.wctc.edu; Database=A11_Movie_SL_22097; User Id=" + user + "; Password=" + pass + ";");

              // IMPORTANT NEED THIS LINE WHEN YOU RUN THE MIGRATION (it can't guess the login/pw) 
              // the ONLY line uncommented should be the updated line below 

             //  optionsBuilder.UseSqlServer(@"Server=bitsql.wctc.edu; Database=TBD_22097; User Id=******; Password=*********;");

            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Humm, something went wrong this time.");
            }

        }

    }


}