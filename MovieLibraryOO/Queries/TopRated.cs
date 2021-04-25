using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MovieLibraryOO.DataModels;
using MovieLibraryOO.CRUD;
using System.IO;
namespace MovieLibraryOO.Queries
{
    public class TopRated
    {
        public void Ranking()
        {
            System.Console.WriteLine("Listing top rated movie by age bracket or occupation");
            System.Console.WriteLine("Sort alphabetically and by rating and display just the first movie");
            Menu menu = new Menu();
            menu.menuSelect();
           
        }
    }
}