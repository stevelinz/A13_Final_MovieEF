using Microsoft.Extensions.DependencyInjection;
using MovieLibraryOO.CRUD;

namespace MovieLibraryOO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // DEPENDENCY INJECTION
            var serviceProvider = new ServiceCollection().BuildServiceProvider();

            Menu menu = new Menu();
            menu.menuSelect();

            // Search search = new Search();
            // search.searchMovie();

            // Add add = new Add();
            // add.AddMovie();
           

           // System.Console.WriteLine("\nThanks for using the Movie Library!");
        }

    }

}