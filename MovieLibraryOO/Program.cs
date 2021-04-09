using Microsoft.Extensions.DependencyInjection;
using MovieLibraryOO.Queries;

namespace MovieLibraryOO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // DEPENDENCY INJECTION
            var serviceProvider = new ServiceCollection().BuildServiceProvider();

            // Search search = new Search();
            // search.searchMovie();

            Add add = new Add();
            add.AddMovie();
           

           // System.Console.WriteLine("\nThanks for using the Movie Library!");
        }

    }

}