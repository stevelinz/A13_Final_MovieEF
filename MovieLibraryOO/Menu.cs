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
            var pickOne = System.Console.ReadLine();
            switch (pickOne)
            {
                case "1":
                case "s":
                nLogger.nLog("New Movie Search");
                 Search search = new Search();
                 search.searchMovie();
                break;
                 case "2":
                 case "a":
                 Add add = new Add();
                 add.AddMovie();
                 break;
                 case "3":
                 case "u":
                 Update update = new Update();
                 update.updateMovie();
                break;
                 case "4":
                 Delete delete = new Delete();
                 delete.deleteMovie();
                 break;
                 case "5":
                 case "d":
                 DisplayMovies display = new DisplayMovies();
                 display.displayAllMovies();
                break;
                case "6":
                AddUser addUser = new AddUser();
                addUser.newUserCreate();    
                break;
                 case "q":
                 case "Q":
                 File.Delete("pass.cnn");
                 File.Delete("user.cnn");
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
            Console.WriteLine("\n\t|    "+" YOUR MOVIE SELECTION"+ "       |");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t| "+"search add update delete show"+ "  |");
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\t   1      2     3     4     5"); 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t|    "+" YOUR QUERY SELECTION"+ "       |");
             Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t| "+"NewUser add update delete quit"+ " |");
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\t   6      7     8     9     Q"); 
            }
        }
    }
}