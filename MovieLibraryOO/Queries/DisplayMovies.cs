using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace MovieLibraryOO.Queries
{
    public class DisplayMovies
    {
        int times;
        int timeSaved;

        public void displayAllMovies()
        {
            NLogger nLogger = new NLogger();
            System.Console.Write("How many movies do you want to see at a time?\t");
            andAgain:
            
            var moviePerTime = Console.ReadLine();
            if (!Int32.TryParse(moviePerTime, out times))
            {
                System.Console.Write("\t Enter a number");
                goto andAgain;
            }
            else if(times == 0 || times > 300){
                System.Console.WriteLine("A number between 1 and 300");
                goto andAgain;
            }
            
            else
            { 
                timeSaved = times;         
                using (var db = new MovieContext())
                {
                    var movieList = db.Movies.Include(x => x.MovieGenres).ThenInclude(x => x.Genre).ToList();
                    int count = 1;
                    
                    foreach (var movie in movieList)
                    {
                        System.Console.WriteLine($"#{count} {movie.Title} {movie.ReleaseDate.ToString("MM-dd-yyyy")}");

                        foreach (var genre in movie.MovieGenres)
                        {
                            System.Console.Write($"\t{genre.Genre.Name}");
                        }
                        count++;
                        System.Console.WriteLine("\n");
                        times--;
                        if(times == 0) 
                        {
                            System.Console.Write("Continue any key or (E)xit to go to Menu?\t");
                            var nextMove = Console.ReadLine();
                            if(nextMove == "E" || nextMove == "e" || nextMove == "Q" || nextMove == "q")
                            {
                                nLogger.nLog("Number of Movies Displayed a once: " + times);
                                Menu menu = new Menu();
                                menu.menuSelect();
                            }
                            else
                            {
                                times = timeSaved;
                                continue;

                            }
                        }
                     }

                }
            }
        }

    }
}
