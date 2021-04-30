using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.IO;

namespace MovieLibraryOO.Queries
{
    public class AddGenre
    {
        Menu menu = new Menu();
        MovieContext db = new MovieContext();
        public void addGenreToMovie()
        {
            NLogger nLogger = new NLogger();

            try
            {
                MovieContext db = new MovieContext();
                System.Console.WriteLine("\nIdentify the Movie to update Genres.");
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Genres will be added never deleted or modified.");
                System.Console.WriteLine("This is because First Impressions count and shall not be forgotten");
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine();
                lookThenUpdate();
                System.Console.Write("Update the movie's Genres by entering it's Movie ID and AND Genre ID or [a]bort:\n");
                viewGenreList();
            andAgain:
                var movId = System.Console.ReadLine();
                if (movId.Equals("a") || movId.Equals("q")) menu.menuSelect();
                int movIdUp;
                if (!Int32.TryParse(movId, out movIdUp))
                {
                    System.Console.Write("\t Entering the movie's ID or [a]bort:\t");
                    goto andAgain;
                }
                else
                {
                    nLogger.nLog("Updating Movie Genre: " + movIdUp);
                    var adjustedTitle = db.Movies.First(d => d.Id == movIdUp);
                    System.Console.Write("Enter the updated Genre(s) or [a]bort\n");
                    System.Console.Write("for the movie:\t");
                    var newMovieTitle = System.Console.ReadLine();
                    if (newMovieTitle.Equals("a") || newMovieTitle.Equals("A") || newMovieTitle.Equals("q")) menu.menuSelect();
                    adjustedTitle.Title = newMovieTitle;
                    db.SaveChanges();
                    menu.menuSelect();
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Opps ... wrong login or password ..... ");
                System.Console.WriteLine("\t ... ( maybe incorrect input) ");
                System.Console.WriteLine("\t ... (Or maybe you forced an exit (Ctl+C)) ");
                System.Console.WriteLine("\tas you started to updating a Movie's Genre(s) ");
                File.Delete("pass.cnn");
                File.Delete("user.cnn");
                System.Environment.Exit(0);
            }
        }
        void lookThenUpdate()
        {
            int count = 0;
        andAgain:

            System.Console.Write("Provide part of the title of the movie, or [a]bort.\t");
            var search = Console.ReadLine();
            if(search == "A" || search == "a" || search == "q") menu.menuSelect();
            if (search.Length < 2) goto andAgain;

            using (db = new MovieContext())
            {
                var movieList = db.Movies

                .Where(x => x.Title.Contains(search))
                .Include(x => x.MovieGenres)
                .ThenInclude(x => x.Genre)
                .ToList();

                count = 1;

                foreach (var movie in movieList)
                {
                    System.Console.Write($"Id: {movie.Id}  {movie.Title}: ");
                    foreach (var genre in movie.MovieGenres)
                    {
                        System.Console.Write($" {genre.Genre.Name} ");
                    }
                    System.Console.WriteLine();
                }
            }

            if (count == 0) System.Console.WriteLine("NO RESULTS FOUND.");
            System.Console.WriteLine("\n");
        }

        void viewGenreList()
        {
            try
            {
                int genreCount = 2;
                using (var db = new MovieContext())
                {
                    var genreView = db.Genres.ToList();
                    foreach (var genList in genreView)
                    {
                        if (genreCount % 2 == 1)
                        {
                            System.Console.Write($"\t\tID: ({genList.Id}) ___ {genList.Name}\n");
                            genreCount++;
                        }
                        else
                        {
                            System.Console.Write($"ID: ({genList.Id}) ___ {genList.Name}");
                            genreCount++;
                        }
                    }
                }
                System.Console.Write("\nEnter the Movie ID of the Movie you want to add to the Genre to or [a]bort\t");
                int movieSelectInt = 0;
            andMovAgain:
                var movieSelect = Console.ReadLine();
                if(movieSelect == "a" || movieSelect == "A" || movieSelect == "q") menu.menuSelect();
                if (!Int32.TryParse(movieSelect, out movieSelectInt))
                {
                    System.Console.Write("\t Enter a number");
                    goto andMovAgain;
                }
                System.Console.Write("\nEnter the ID of the Genre you want to add\t");
                int genreSelectInt = 0;
            andGenAgain:
                var genreSelect = Console.ReadLine();
                if (!Int32.TryParse(genreSelect, out genreSelectInt))
                {
                    System.Console.Write("\t Enter a number");
                    goto andGenAgain;
                }

                var query = "INSERT INTO  MovieGenres (GenreId, MovieId) VALUES ("
                + genreSelectInt + "," + movieSelectInt + ")";

                using (var db = new MovieContext())
                {
                    db.Database.ExecuteSqlRaw(query);
                }
              
                menu.menuSelect();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Something went wrong add a Genre please try again");
                menu.menuSelect();
            }
        }
    }

}