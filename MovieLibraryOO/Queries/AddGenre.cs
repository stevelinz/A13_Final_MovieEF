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
        public int movieSelectInt = 0;
        public int genreSelectInt = 0;
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
                lookOnly();
                System.Console.Write("Insert a movie Genre by entering it's Movie ID and AND Genre ID or [a]bort:\n");
                viewGenreList();
                andAgain:
                System.Console.Write("\nEnter  Movie ID of the Movie you want to add a Genre to or [a]bort\t");
                var movId = System.Console.ReadLine();
                if (movId.Equals("a") || movId.Equals("A") || movId.Equals("q")) menu.menuSelect();
                int movIdInt;
                if (!Int32.TryParse(movId, out movIdInt))
                {
                    System.Console.Write("\t Entering the movie's ID or [a]bort:\t");
                    goto andAgain;
                }
                
                    nLogger.nLog("Insert a Movie Genre for Movie Id: " + movIdInt);
                    
                    System.Console.Write("\nEnter the ID of the Genre you want to add\t");

                    andGenAgain:

                    var genreSelect = Console.ReadLine();
                    if (genreSelect.Equals("a") || genreSelect.Equals("A") || genreSelect.Equals("q")) menu.menuSelect();
                    
                    if (!Int32.TryParse(genreSelect, out genreSelectInt))
                    {
                        System.Console.Write("\t Enter a number");
                        goto andGenAgain;
                    }

                    else
                    {
                        var query = "INSERT INTO  MovieGenres (GenreId, MovieId) VALUES ("
                        + genreSelectInt + "," + movIdInt + ")";

                        using (var db2 = new MovieContext())
                        {
                            db2.Database.ExecuteSqlRaw(query);
                        }
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
        void lookOnly()
        {
            int count = 0;
            andAgain:

            System.Console.Write("Provide part of the title of the movie to consider, or [a]bort.\t");
            var search = Console.ReadLine();
            if (search == "A" || search == "a" || search == "q") menu.menuSelect();
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

            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Something went wrong add a Genre please try again");
                menu.menuSelect();
            }
        }
    }

}