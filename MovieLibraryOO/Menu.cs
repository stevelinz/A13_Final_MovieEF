using System;
using System.IO;
using MovieLibraryOO.CRUD;
using MovieLibraryOO.Queries;

namespace MovieLibraryOO
{
    public class Menu
    {
        public void menuSelect()
        {
        goAgain:
            startUp();
            NLogger nLogger = new NLogger();
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "rate.sav"), "rate");  //recent
            var pickOne = System.Console.ReadLine();
            switch (pickOne)
            {
                case "1":
                case "s":
                    nLogger.nLog("Menu: Movie Search");
                    Search search = new Search();
                    search.searchMovie();
                    break;
                case "2":
                case "a":
                    nLogger.nLog("Menu: Movie Add");
                    Add add = new Add();
                    add.AddMovie();
                    break;
                case "3":
                case "u":
                    nLogger.nLog("Menu: Movie Update");
                    Update update = new Update();
                    update.updateMovie();
                    break;
                case "4":
                    nLogger.nLog("Menu: Movie Delete");
                    Delete delete = new Delete();
                    delete.deleteMovie();
                    break;
                case "5":
                case "d":
                    nLogger.nLog("Menu: Movie Display");
                    DisplayMovies display = new DisplayMovies();
                    display.displayAllMovies();
                    break;
                case "6":
                    nLogger.nLog("Menu: Add User");
                    AddUser addUser = new AddUser();
                    addUser.newUserCreate();
                    break;
                case "7":
                    nLogger.nLog("Menu: User Rating");
                    UserRating userRating = new UserRating();
                    userRating.UserRatesMovie();
                    break;
                case "8":
                    nLogger.nLog("Menu: Top Rated");
                    TopRated topRated = new TopRated();
                    topRated.Ranking();
                    break;
                case "9":
                    nLogger.nLog("Menu: Add Genre");
                    AddGenre addGenre = new AddGenre();
                    addGenre.addGenreToMovie();
                    break;
                case "q":
                case "Q":
                    nLogger.nLog("Menu: Quit");
                    System.Console.WriteLine("Closing the connection and exiting the program.");
                    File.Delete("pass.cnn");
                    File.Delete("user.cnn");
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "rate.sav"), "rate");  //recent
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.Write("\t Wrong input.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto goAgain;
            }
            startUp();
            void startUp()
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\t\t|       " + " YOUR MOVIE SELECTION" + "        |");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t\t|    " + "search add update delete show" + "   |");
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine("\t\t      1      2     3     4     5");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\t\t|       " + " YOUR QUERY SELECTION" + "        |");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t\t| " + "New(User) Rate Ranking Genre  Quit" + " |");
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine("\t\t   6         7     8      9     Q");
            }
        }
    }
}