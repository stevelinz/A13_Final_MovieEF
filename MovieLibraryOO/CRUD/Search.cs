using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieLibraryOO.Context;
using MovieLibraryOO.Queries;

namespace MovieLibraryOO.CRUD
{
    public class Search
    {
        public void searchMovie()
        {
            NLogger nLogger = new NLogger();
            Menu menu = new Menu();
            try
            {
                int count = 0;
                andAgain:
                MovieContext db = new MovieContext();
               
                System.Console.Write("Provide part of the Movie title or year of Movie: \t");
                var search = Console.ReadLine();
                if (search.Length < 2) goto andAgain;
                var movieList = db.Movies
                .FromSqlRaw($"SELECT * FROM dbo.Movies where Title like '%" + search + "%'").ToList();

                foreach (var showMovie in movieList)
                {
                    if(count < 9)
                    {
                        System.Console.WriteLine("[0" + (count + 1) + "] ID: " 
                        + showMovie.Id + " " + showMovie.Title);
                    }
                    else
                    {
                        System.Console.WriteLine("[" + (count + 1) + "] ID: " 
                        + showMovie.Id + " " + showMovie.Title);
                    }
                    count++;
                }
                System.Console.WriteLine("\n" + count + " movie(s) fit this search. ");
                System.Console.WriteLine("\n"); 

                System.Console.Write("Do you want to [S]earch Movies again?\t");
                System.Console.Write("([Enter] to Continue to the next action)\t");
                var reSearch = Console.ReadLine();
                UserRating userRating = new UserRating(); 
                string testRate = System.IO.File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "rate.sav"));
                if(reSearch == "S" || reSearch == "s" || reSearch == "Y" || reSearch == "y" || reSearch == "a")
                {
                   count = 0;
                   goto andAgain; 
                }

                if(!testRate.Equals("rate"))
                {
                     userRating.rateReturn();
                }   
                else
                { 
                   nLogger.nLog("Leaving Movie Search");
                   menu.menuSelect();
                }
                 
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
