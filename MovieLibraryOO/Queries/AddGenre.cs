using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MovieLibraryOO.DataModels;
using System.IO;

namespace MovieLibraryOO.Queries
{
    public class AddGenre
    {

         public void addGenreToMovie()
        {
            NLogger nLogger = new NLogger();
            Menu menu = new Menu();
            try
            {
                MovieContext db = new MovieContext();
                System.Console.WriteLine("\nIdentify the Movie to update Genres.");
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Genres will only be added never deleted or modified.");
                System.Console.WriteLine("This is because First Impressions count and shall not be forgotten");
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine();
                lookThenUpdate();
                System.Console.Write("Update the movie by entering it's ID or [a]bort:\t");
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
                    nLogger.nLog("Updating Movie Id: " + movIdUp);
                    var adjustedTitle = db.Movies.First(d => d.Id == movIdUp);
                    System.Console.Write("Enter the updated title or [a]bort\n");
                    System.Console.Write("for the movie:\t");
                    var newMovieTitle = System.Console.ReadLine();
                    if (newMovieTitle.Equals("a") || newMovieTitle.Equals("q")) menu.menuSelect();
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
                System.Console.WriteLine("\tas you started to updating a Movie ");
                File.Delete("pass.cnn");
                File.Delete("user.cnn");
                System.Environment.Exit(0);
            }
        }
        void lookThenUpdate()
        {
            int count = 0;
            MovieContext db = new MovieContext();
            andAgain:

            System.Console.Write("Provide part of the title of the movie.\t");
            var search = Console.ReadLine();
            if (search.Length < 2) goto andAgain;
            var movieList = db.Movies
            .FromSqlRaw($"SELECT * FROM dbo.Movies where Title like '%" + search + "%'").ToList();
            foreach (var showMovie in movieList)
            {
                System.Console.WriteLine("ID: " + showMovie.Id + " " + showMovie.Title);
                count++;
            }
            if (count == 0) System.Console.WriteLine("NO RESULTS FOUND.");
            System.Console.WriteLine("\n");
        }
    }

      
        
    
}