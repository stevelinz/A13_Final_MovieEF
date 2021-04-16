using System;
using System.IO;
using MovieLibraryOO.CRUD;
namespace MovieLibraryOO
{
    public class Menu
    {
        public void menuSelect()
        {
            goAgain:
            startUp();
            var pickOne = System.Console.ReadLine();
            switch (pickOne)
            {
                case "1":
                case "s":
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
                 case "d":
                 Delete delete = new Delete();
                 delete.deleteMovie();
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
            Console.WriteLine("\t| "+"search add update delete quit"+ "  |");
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\t   1      2     3     4     Q"); 
            }
        }
    }
}