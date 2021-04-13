using System.Data.Common;
using System;
using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;

namespace MovieLibraryOO.CRUD
{
    public class Delete
    {
        public void deleteMovie()
        {
            try
            {

                MovieContext db = new MovieContext();
                Menu menu = new Menu();
                System.Console.WriteLine("\tIdentify the Movie to delete.");
                lookThenDelete();
                System.Console.Write("\tDelete the movie by entering it's ID or [a]bort:\t");
                var movId = System.Console.ReadLine();
                if(movId.Equals("a")||movId.Equals("q")) menu.menuSelect();
                int movIdDel = Int32.Parse(movId);

                db.Remove(db.Movies.Single(d => d.Id == movIdDel));
                db.SaveChanges();
                menu.menuSelect();
            }

            catch (System.Exception)
            {
                System.Console.WriteLine("Opps ... wrong login or password ....");
                System.Console.WriteLine("\t....(Or maybe you forced an exit (Ctl+C)) ");
                System.Console.WriteLine("\t...........(Or maybe an incorrect input) ");
                System.Console.WriteLine("\t.....as you started to delete a Movie " + "\U0001F914");
                File.Delete("pass.cnn");
                File.Delete("user.cnn");
                System.Environment.Exit(0);
            }
        }
        void lookThenDelete()
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
