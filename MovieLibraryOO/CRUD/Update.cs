using System;
using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;

namespace MovieLibraryOO.CRUD
{
    public class Update
    {
        public void updateMovie()
        {
            Menu menu = new Menu();
            try
            {
                MovieContext db = new MovieContext();
                System.Console.WriteLine("\nIdentify the Movie to update.");
                lookThenUpdate();
                System.Console.Write("Update the movie by entering it's ID or [a]bort:\t");
                var movId = System.Console.ReadLine();
                if(movId.Equals("a")||movId.Equals("q")) menu.menuSelect();
                int movIdDel = Int32.Parse(movId);
                var adjustedTitle = db.Movies.First(d => d.Id == movIdDel);
                System.Console.Write("Enter the updated title or [a]bort\n");
                System.Console.Write("for the movie:\t");
                var newMovieTitle = System.Console.ReadLine();
                if(newMovieTitle.Equals("a")||newMovieTitle.Equals("q")) menu.menuSelect(); 
                adjustedTitle.Title = newMovieTitle;
                db.SaveChanges();
                menu.menuSelect();
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
                System.Console.WriteLine("ID: " + showMovie.Id + "\t" + showMovie.Title);
                count++;
            }
            if (count == 0) System.Console.WriteLine("NO RESULTS FOUND.");
            System.Console.WriteLine("\n");
        }
    }
}
