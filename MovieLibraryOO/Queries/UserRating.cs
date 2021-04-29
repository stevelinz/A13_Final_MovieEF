using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MovieLibraryOO.DataModels;
using MovieLibraryOO.CRUD;
using System.IO;
namespace MovieLibraryOO.Queries
{
    public class UserRating
    {
        public void UserRatesMovie()
        {
            NLogger nLogger = new NLogger(); 
            try
            {
                Menu menu = new Menu();
                var userId = "";
                int userIdInt = 0;
                System.Console.Write("Rate a Movie ");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Wizard");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("Step 1 of 4: Identify User?\t\t");
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write("What is your User Id?\t");
                andAgain:
                userId = Console.ReadLine();
                if (!Int32.TryParse(userId, out userIdInt))
                {
                    System.Console.Write("\t Entering the User's ID\t");
                    goto andAgain;
                }
                nLogger.nLog("User Id : " + userIdInt);
                using (var db = new MovieContext())
                {
                    int count = 0;
                    var users = db.Users.Include(x => x.Occupation)
                                        .Where(x => x.Id == userIdInt).ToList();

                    foreach (var user in users)
                    {
                        if (user.Id == userIdInt) count = 1;

                        System.Console.WriteLine($"Welcome user: ({user.Id}) Sex: {user.Gender} Age: {user.Age} Zip: {user.ZipCode} {user.Occupation.Name}");
                        File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "rate.sav"), userId);

                    }
                    if (count == 0)
                    {
                        System.Console.WriteLine("User Not Found, Returning to Menu");
                        File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "rate.sav"), "rate");
                        menu.menuSelect();
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    System.Console.Write("Step 2 of 4: Search Movie\t\t");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Console.Write("Find the Movie to Rate. Hit any key\t");
                    Console.ReadKey();
                    System.Console.WriteLine();
                    Search search = new Search();
                    search.searchMovie();

                }
            }
            catch (System.Exception)
            {

                System.Console.WriteLine("Problem with the Rating process, returning to Menu");
                Menu menu = new Menu();
                menu.menuSelect();
            }
        }
        public bool rateReturn()
        {
            try
            {

                Menu menu = new Menu();
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("Step 3 of 4: Select Movie\t\t");
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write("Select the Movie Id to Rate enter it's ID or [a]bort:\t");
                andAgain:
                var movId = System.Console.ReadLine();
                if (movId.Equals("a") || movId.Equals("q")) menu.menuSelect();
                int movIdInt;
                if (!Int32.TryParse(movId, out movIdInt))
                {
                    System.Console.Write("\t Entering the movie's ID or [a]bort:\t");
                    goto andAgain;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("Step 4 of 4: Select Rating\t\t");
                Console.ForegroundColor = ConsoleColor.White;

                System.Console.WriteLine("\n1 star  - hated it ");
                System.Console.WriteLine("2 stars - didn't like it");
                System.Console.WriteLine("3 stars - it was ok");
                System.Console.WriteLine("4 stars - really liked it");
                System.Console.WriteLine("5 stars - loved it and I would watch again");

                System.Console.Write("How do you feel about this movie?\t");
                andRateAgain:
                int userRateInt = 0;
                var userRate = Console.ReadLine();
                if (!Int32.TryParse(userRate, out userRateInt))
                {
                    System.Console.Write("\t Entering the User's ID\t");
                    goto andRateAgain;
                }
                using (var db = new MovieContext())
                {
                    System.DateTime date = DateTime.Now;
                    string userWhoRates = System.IO.File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "rate.sav"));
                    var userRates = db.UserMovies.Include(x => x.User).Include(x => x.Movie).FirstOrDefault();

                    userRates = new UserMovie { Rating = userRateInt, RatedAt = date };

                    db.Add(userRates);
                    db.SaveChanges();

                    db.Database.ExecuteSqlInterpolated($"UPDATE UserMovies SET UserId = {userWhoRates}, MovieId = {movIdInt} WHERE Id = {userRates.Id} ");

                    db.SaveChanges();
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "rate.sav"), "rate");

                    System.Console.WriteLine($"User: {userWhoRates} submitted a {userRateInt} star(s) rating at {date} for Movie Id: {movIdInt}");

                    menu.menuSelect();
                }

                return true;
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Problem with the Rating creating process, returning to Menu");
                Menu menu = new Menu();
                menu.menuSelect();
                return true;
            }

        }

    }

}












