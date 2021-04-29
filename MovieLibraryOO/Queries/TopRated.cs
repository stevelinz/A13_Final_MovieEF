using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MovieLibraryOO.DataModels;
using MovieLibraryOO.CRUD;
using System.IO;
namespace MovieLibraryOO.Queries
{
    public class TopRated
    {


        public void Ranking()
        {
            try
            {

                using (var db = new MovieContext())
                {
                    int dbCount = db.Occupations.Count();
                    int count = dbCount;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    System.Console.WriteLine("\t\tTREND DASHBOARD\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
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
                                System.Console.Write("<============ any key for More Occupations ===============>\t");
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

            catch (System.Exception e)
            {
                System.Console.WriteLine(e);
            }

        }
    }


}
