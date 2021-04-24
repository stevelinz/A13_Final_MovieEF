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
                    System.Console.WriteLine("User Not Found");
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
            public bool rateReturn()
            {
                Menu menu = new Menu();
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write("Step 3 of 4: Select Movie\t\t");
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write("Select the movie to Rate entering it's ID or [a]bort:\t");
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
                System.IO.File.ReadAllText(Path.Combine(System.Environment.CurrentDirectory, "rate.sav")); 

              System.Console.WriteLine("1 star  - hated it ");
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
                  //  var user = db.Users.Include(x => x.Occupation).FirstOrDefault();

                    // user = new User { Age = ageInt, Gender = gender, ZipCode = zip };

                    // db.Add(user);

                    // db.SaveChanges();

                    // idUsed = user.Id;

                  //  db.Database.ExecuteSqlInterpolated($"UPDATE Users SET occupationId =  {occInt} WHERE Id = {user.Id} ");

                  //  db.SaveChanges();
                }

                

               


                return true;
            }
              
                
            }

        }

        //if not exist
        //goAgain
        //else [store User]
        //ask user to search movies
        //if found
        //ask user to enter ID
        //else searchAgain
        //user assigns rating
        // ratedAt time assigned
        // = display user
        // = movie
        // = rating

    



