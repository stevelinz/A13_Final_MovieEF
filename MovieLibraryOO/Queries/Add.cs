using System;
using MovieLibraryOO.Context;
using MovieLibraryOO.DataModels;

namespace MovieLibraryOO.Queries
{
    public class Add
    {

        System.DateTime date = DateTime.Now;
        public void AddMovie()
        {
       using (var db = new MovieContext())
            {
              
                    db.Movies.Add(new Movie { Title = "newTitle",  ReleaseDate = date });
                   
                    db.SaveChanges();
            }

        }
    }
}

    