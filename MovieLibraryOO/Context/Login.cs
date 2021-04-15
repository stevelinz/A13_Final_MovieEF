using System;
using System.IO;
using MovieLibraryOO.Queries;

namespace MovieLibraryOO
{
    public class Login
    {
        // to create a username/password system: 2 .cnn files are written
        // these files are deleted at exit
        private string userName = "";
        private string passWord = "";
        public string userPath = Path.Combine(Environment.CurrentDirectory, "user.cnn");
        public string passPath = Path.Combine(Environment.CurrentDirectory, "pass.cnn");
        public void userConnectionString()
        {
            System.Console.WriteLine("Welcome to the Movie Database");

            System.Console.Write("Enter User Name: ");
            userName = Console.ReadLine();

            System.Console.Write("Enter Password: ");
            passWord = Console.ReadLine();

            File.WriteAllText(userPath, userName);
            File.WriteAllText(passPath, passWord);

            Count count = new Count();
            count.countMovie();

            Menu menu = new Menu();
            menu.menuSelect();

        }
    }
}