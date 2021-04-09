using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieLibraryOO.Context;

namespace MovieLibraryOO.CRUD
{
    public class Search
    {
        public void searchMovie()
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
                System.Console.WriteLine(showMovie.Title);
                count++;
            }

            if (count == 0) System.Console.WriteLine("NO RESULTS FOUND.");

            System.Console.WriteLine("\n");

            goto andAgain;

        }
    }
}
