using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MovieLibraryOO.DataModels;

namespace MovieLibraryOO.Queries
{
    public class AddGenre
    {
         public void addGenreToMovie()
        {
            System.Console.WriteLine("Add Genres to Movies");
            Menu menu = new Menu();
            menu.menuSelect();
        }
    }
}