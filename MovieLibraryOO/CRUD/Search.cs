using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieLibraryOO.Context;

namespace MovieLibraryOO.CRUD
{
    public class Search
    {
        public void searchMovie()
        {
            Menu menu = new Menu();
            try
            {
                int count = 0;
               MovieContext db = new MovieContext();
           andAgain:
                System.Console.Write("\tProvide part of the title: \t");
                var search = Console.ReadLine();
                if (search.Length < 2) goto andAgain;
                var movieList = db.Movies
                .FromSqlRaw($"SELECT * FROM dbo.Movies where Title like '%" + search + "%'").ToList();
                System.Console.WriteLine("\t――――――――――――――――――――――――――――――――――");
                foreach (var showMovie in movieList)
                {
                    System.Console.WriteLine("ID: " + showMovie.Id + " " + showMovie.Title);
                    count++;
                }
                System.Console.WriteLine("\n" + count + " movie(s) fit this search. ");
                System.Console.WriteLine("\n");             
                menu.menuSelect();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Opps ... wrong login or password .... ");
                System.Console.WriteLine("\t...(Or maybe you forced an exit (Ctl+C)) ");
                System.Console.WriteLine("\t...(Or maybe an incorrect input  ");
                System.Console.WriteLine("\tas you started to look-up Movies ");
                File.Delete("pass.cnn");
                File.Delete("user.cnn");
                System.Environment.Exit(0);
            }

        }
    }
}
