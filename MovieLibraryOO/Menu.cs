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
            var pickOne = System.Console.ReadKey();
            Console.TreatControlCAsInput = true;
            switch (pickOne.KeyChar)
            {
                case '1':
                 Search search = new Search();
                 search.searchMovie();
                break;
                 case '2':
                 Add add = new Add();
                 add.AddMovie();
                 break;
                 case '3':
                 Update update = new Update();
                 update.updateMovie();
                break;
                 case '4':
                 Delete delete = new Delete();
                 delete.deleteMovie();
                break;
                 case 'q':
                 File.Delete("pass.cnn");
                 File.Delete("user.cnn");
                 System.Environment.Exit(0);
                 break;
                default:
                System.Console.WriteLine(" ← Wrong input.");
                goto goAgain;
            }
            startUp();
            void startUp()
            {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\t▍    "+" YOUR MOVⅠE SELEⅭTⅠON"+ "       ▍");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t▍ "+"search add update delete quit"+ "  ▍");
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\t   ①      ②     ③     ④     Ⓠ"); 
            }
        }
    }
}