using System;
using MovieLibraryOO.Context;
using MovieLibraryOO.DataModels;

namespace MovieLibraryOO.CRUD
{
    public class Add
    {

        System.DateTime date = DateTime.Today;
        public void AddMovie()
        {

            addExplain();
            var addMovieInput = Console.ReadLine();
            using (var db = new MovieContext())
            {


                db.Movies.Add(new Movie { Title = addMovieInput, ReleaseDate = date });

                db.SaveChanges();
            }

        }
        void addExplain()
        {
            System.Console.WriteLine("\tType the title of the new movie.");
            System.Console.WriteLine("\tYou can add the year written");
            System.Console.Write("\t(YYYY) at the end:\t");
        }
    }
}

