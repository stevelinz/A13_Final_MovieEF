using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            passWord = Orb.App.Console.ReadPassword();

            File.WriteAllText(userPath, userName);
            File.WriteAllText(passPath, passWord);

            Count count = new Count();
            count.countMovie();

                Menu menu = new Menu();
                menu.menuSelect();

            }
        }

        namespace Orb.App
    {

        static public class Console
        {
            
            /// <param name="mask">a <c>char</c> representing your choice of console mask</param>
            /// <returns>the string the user typed in </returns>
            public static string ReadPassword(char mask)
            {
                const int ENTER = 13, BACKSP = 8, CTRLBACKSP = 127;
                int[] FILTERED = { 0, 27, 9, 10 /*, 32 space, if you care */ }; // const

                var pass = new Stack<char>();
                char chr = (char)0;

                while ((chr = System.Console.ReadKey(true).KeyChar) != ENTER)
                {
                    if (chr == BACKSP)
                    {
                        if (pass.Count > 0)
                        {
                            System.Console.Write("\b \b");
                            pass.Pop();
                        }
                    }
                    else if (chr == CTRLBACKSP)
                    {
                        while (pass.Count > 0)
                        {
                            System.Console.Write("\b \b");
                            pass.Pop();
                        }
                    }
                    else if (FILTERED.Count(x => chr == x) > 0) { }
                    else
                    {
                        pass.Push((char)chr);
                        System.Console.Write(mask);
                    }
                }

                System.Console.WriteLine();

                return new string(pass.Reverse().ToArray());
            }
     
            public static string ReadPassword()
            {
                return Orb.App.Console.ReadPassword('*');
            }
        }
    }
}
