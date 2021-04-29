using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
namespace MovieLibraryOO.Queries
{
    public class TopRated
    {
        public int min = 0;
        public int max = 0;
        NLogger nLogger = new NLogger();
        public void rankingHub()
        {

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            System.Console.WriteLine("\t\tTREND DASHBOARD\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.Write("View by [O]ccupations or by [A]ge bracket?:\t");
            var picked = Console.ReadLine();
            if (picked == "O" || picked == "o" || picked == "0" || picked == "j")
            {
                nLogger.nLog("View ranking by Occupation");
                occupationRanking();
            }
            else
            {
                nLogger.nLog("View Ranking by Age");
                bracket();
            }
        }
        public static void occupationRanking()
        {
            try
            {
                using (var db = new MovieContext())
                {
                    int dbCount = db.Occupations.Count();
                    int count = dbCount;
                    System.Console.WriteLine("\tTOP RATED FIVE STAR MOVIES BY USER OCCUPATION");
                    System.Console.WriteLine("\t\t(In reverse alphabetical order)\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    int half = 0;
                next:
                    var selectedUser = db.Users.Where(x => x.Occupation.Id == count);
                    var users = selectedUser.Include(x => x.UserMovies).ThenInclude(x => x.Movie).ToList();

                    foreach (var user in users)
                    {
                        System.Console.Write($" {user.Occupation.Id}\t{user.Occupation.Name.Trim()}: ");

                        foreach (var movie in user.UserMovies.Where(x => x.Rating == 5).Take(1))
                        {
                            System.Console.WriteLine(String.Format("{0,-10}", $"{movie.Movie.Title}"));
                            System.Console.WriteLine();
                        }
                        count--;
                        if (count == 0)
                        {
                            break;
                        }
                        else
                        {
                            if (half == 12)
                            {
                                System.Console.Write("<============ Enter key for More Occupations ===============>\t");
                                System.Console.WriteLine();
                                Console.ReadKey();
                                System.Console.WriteLine();
                            }
                            half++;
                            goto next;
                        }
                    }
                }

                Menu menu = new Menu();
                menu.menuSelect();

            }

            catch (System.Exception)
            {
                System.Console.WriteLine("Something went wrong viewing top rate movies by occupation");
                System.Console.WriteLine("Returning to menu.");
                Menu menu = new Menu();
                menu.menuSelect();

            }
        }
        private void ageRanking()
        {
            try
            {
                using (var db = new MovieContext())
                {
                    var selectedUser = db.Users.Where(x => x.Age > min && x.Age < max);
                    var users = selectedUser.Include(x => x.UserMovies).ThenInclude(x => x.Movie).OrderBy(x=>x.Age).ToList();

                    foreach (var user in users)
                    {
                        System.Console.Write($"Age: {user.Age} {user.Gender} {user.Occupation.Name.Trim()}: ");

                        foreach (var movie in user.UserMovies.Where(x => x.Rating == 5).Take(1))
                        {
                            System.Console.WriteLine(String.Format("{0,-10}", $"{movie.Movie.Title}"));
                        }
                    }
                }
                Menu menu = new Menu();
                menu.menuSelect();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Something went wrong viewing top rate movies by age");
                System.Console.WriteLine("Returning to menu.");
                Menu menu = new Menu();
                menu.menuSelect();
            }

        }
        private void bracket()
        {
            System.Console.WriteLine("\t\tSelect by Age Bracket\n");
            goAgain:
            Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine("\tDisplay in order by age.");
            Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.Write("[K]id\t[Y]outh\t[M]iddle-Aged\t[S]enior:\t");
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            Console.ForegroundColor = ConsoleColor.White;
            var agePicked = Console.ReadLine();

            switch (agePicked)
            {
                 // 0–14 years old(pediatric group), Kids
                case "K":
                case "k":
                case "1":
                    min = 0;
                    max = 14;
                    nLogger.nLog("Rank: Kids");
                    break;
                 // 15–47 years old(youth group), Youth
                case "Y":
                case "y":
                case "2":
                    min = 15;
                    max = 47;
                    nLogger.nLog("Rank: Youth");
                    break;
                // 48–63 years old(middle-aged group) Middle-Aged
                case "M":
                case "m":
                case "3":
                    min = 48;
                    max = 63;
                    nLogger.nLog("Rank: Middle-Age");
                    break;
                // and ≥ 64 years old(elderly group). Senior
                case "S":
                case "s":
                case "4":
                    min = 64;
                    max = 120;
                    nLogger.nLog("Rank: Senior");
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.Write("\t Wrong input.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto goAgain;

            }
            ageRanking();

        }

    }
}
