using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using System;

namespace MovieLibraryOO.Queries
{
    public class Count
    {
        public void countMovie()
        {
            try
            {

                MovieContext db = new MovieContext();
                
                var movieList = db.Movies
                .FromSqlRaw($"SELECT * FROM dbo.Movies")
                .ToList();

                var movieCount = movieList.Count();

                System.Console.WriteLine("Movies in the database: " + movieCount);

                var menu = new Menu();
                menu.menuSelect();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("\nWrong login or password");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("CAN NOT OPEN THE DATABASE\t"+"\n");
                File.Delete("pass.cnn");
                File.Delete("user.cnn");
                Console.ForegroundColor = ConsoleColor.White;
                System.Environment.Exit(0);
            }
        }
    }
}
