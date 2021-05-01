using System;
using System.IO;
using MovieLibraryOO.Context;
using MovieLibraryOO.DataModels;

namespace MovieLibraryOO.CRUD
{
    public class Add
    {
        public void AddMovie()
        {
            Menu menu = new Menu();
            NLogger nLogger = new NLogger();
            try
            {
                System.DateTime date = DateTime.Today;
                addExplain();
                var addMovieInput = Console.ReadLine();
                nLogger.nLog("Added: " + addMovieInput);
                if (addMovieInput.Equals("a") || addMovieInput.Equals("q"))
                {
                    menu.menuSelect();
                }
                else
                {
                    using (var db = new MovieContext())
                    {
                        var movie = new Movie { Title = addMovieInput, ReleaseDate = date };
                        db.Add(movie);
                        nLogger.nLog("Movie Add Committed");
                        db.SaveChanges();
                        menu.menuSelect();
                    }
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Wrong login or password ...");
                System.Console.WriteLine("\t...(Or maybe you forced an exit (Ctl+C)) ");
                System.Console.WriteLine("\t......(Or incorrect input) ");
                System.Console.WriteLine("\t.........as you started to add a Movie ");
                File.Delete("pass.cnn");
                File.Delete("user.cnn");
                System.Environment.Exit(0);
            }
        }
        void addExplain()
        {
            System.Console.WriteLine("\tType the title of the new movie.");
            System.Console.WriteLine("\tYou can add the year written");
            System.Console.Write("\t(YYYY) at the end or [a]bort:\t");
        }
    }
}

