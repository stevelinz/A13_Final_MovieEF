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
            try
            {
                System.DateTime date = DateTime.Today;         
                addExplain();
                var addMovieInput = Console.ReadLine();
                if(addMovieInput.Equals("a")||addMovieInput.Equals("q")) menu.menuSelect();
                using (var db = new MovieContext())
                {
                    db.Movies.Add(new Movie { Title = addMovieInput, ReleaseDate = date });
                    db.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Opps ... wrong login or password ...");
                System.Console.WriteLine("\t...(Or maybe you forced an exit (Ctl+C)) ");
                System.Console.WriteLine("\t......(Or incorrect input) ");
                System.Console.WriteLine("\t.........as you started to add a Movie " + "\U0001F914");
                File.Delete("pass.cnn");
                File.Delete("user.cnn");
                System.Environment.Exit(0);
            }
            
            menu.menuSelect();
        }
        void addExplain()
        {
            System.Console.WriteLine("\tType the title of the new movie.");
            System.Console.WriteLine("\tYou can add the year written");
            System.Console.Write("\t(YYYY) at the end or [a]bort:\t");
        }
    }
}

