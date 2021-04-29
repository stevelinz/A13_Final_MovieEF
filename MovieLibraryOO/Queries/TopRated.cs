using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
namespace MovieLibraryOO.Queries
{
    public class TopRated
    {
        public void rankingHub()
        {
            NLogger nLogger = new NLogger();
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
                ageRanking();
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
                    System.Console.WriteLine("\tTOP RATED FIVE STAR MOVIES BY USER OCCUPATION\n");
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
        private static void ageRanking()
        {
            try
            {
                using (var db = new MovieContext())
                {
                    var selectedUser = db.Users.Where(x => x.Age > 46 && x.Age < 49);
                    var users = selectedUser.Include(x => x.UserMovies).ThenInclude(x => x.Movie).ToList();

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
        private static void bracket()
        {
            System.Console.WriteLine("Select by Age Bracket");

            // 0–14 years old(pediatric group), 
            // 15–47 years old(youth group), 
            // 48–63 years old(middle-aged group) 
            // and ≥ 64 years old(elderly group). 

        }

    }
}
