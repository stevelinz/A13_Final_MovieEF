using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MovieLibraryOO.DataModels;

namespace MovieLibraryOO.Queries
{
    public class AddUser
    {
        long idUsed = 0;
        public void newUserCreate()
        {
            Menu menu = new Menu();

            try
            {
                var age = "";
                var ageInt = 0;
                var gender = "";
                var zip = "";
                var occ = "";
                var occInt = 0;
                System.Console.Write("Create a new User ");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Wizard");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.Write("Step 1 of 4: Age?\t\t");
                Console.ForegroundColor = ConsoleColor.White;
                andAgain:
                age = Console.ReadLine();
                if (!Int32.TryParse(age, out ageInt))
                {
                    System.Console.Write("\t Enter a number");
                    goto andAgain;
                }
                else if (ageInt == 0 || ageInt > 120)
                {
                    System.Console.WriteLine("An age between 1 and 120");
                    goto andAgain;
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.Write("Step 2 of 4: Gender? [M/F]\t");
                Console.ForegroundColor = ConsoleColor.White;

                andTryAgain:
                gender = Console.ReadLine();
                if (gender.Equals("M") || gender.Equals("m") || gender.Equals("F") || gender.Equals("f"))
                {
                    if (gender.Equals("m")) gender = "M";
                    if (gender.Equals("f")) gender = "F";
                }
                else
                {
                    System.Console.Write("\t Enter either \"M\" or \"F\" ");
                    gender = "";
                    goto andTryAgain;
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.Write("Step 3 of 4: Zipcode?\t\t");
                Console.ForegroundColor = ConsoleColor.White;

                andTryZipAgain:
                zip = Console.ReadLine();
                if (zip.Length < 5)
                {
                    System.Console.Write("\t Zipcodes are at least 5 characters ");
                    goto andTryZipAgain;
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.Write("Step 4 of 4: Occupation?");
                Console.ForegroundColor = ConsoleColor.White;

                int occCount = 2;
                using (var db = new MovieContext())
                {
                    var occupationVar = db.Occupations.ToList();
                    foreach (var occList in occupationVar)
                    {
                        if (occCount == 2) System.Console.WriteLine();
                        if (occCount == 3)
                        {
                            System.Console.Write($" \tID: ({occList.Id}) for {occList.Name}\n");
                            occCount++;
                            continue;
                        }

                        if (occCount % 2 == 1)
                        {
                            System.Console.Write($"\t\tID: ({occList.Id}) for {occList.Name}\n");
                            occCount++;
                        }
                        else
                        {
                            System.Console.Write($"ID: ({occList.Id}) for {occList.Name}");
                            occCount++;
                        }
                    }
                }
                System.Console.Write("\nSelect the ID of user's occupation\t");

                andOccAgain:
                occ = Console.ReadLine();
                if (!Int32.TryParse(occ, out occInt))
                {
                    System.Console.Write("\t Enter a number");
                    goto andOccAgain;
                }

                // end of gathering new user data

                using (var db = new MovieContext())
                {
                    var user = db.Users.Include(x => x.Occupation).FirstOrDefault();

                    user = new User { Age = ageInt, Gender = gender, ZipCode = zip };

                    db.Add(user);

                    db.SaveChanges();

                    idUsed = user.Id;

                    db.Database.ExecuteSqlInterpolated($"UPDATE Users SET occupationId =  {occInt} WHERE Id = {user.Id} ");

                    db.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Something has gone wrong adding a user, please login and try again.");
            }

            using (var db = new MovieContext())
            {

                var users = db.Users.Include(x => x.Occupation)
                                    .Where(x => x.Id == idUsed).ToList();
                foreach (var user in users)
                {
                     Console.ForegroundColor = ConsoleColor.DarkRed;
                    System.Console.WriteLine($"User Added: ({user.Id}) Age: {user.Age} Sex: {user.Gender} Zip: {user.ZipCode} {user.Occupation.Name}");
                     Console.ForegroundColor = ConsoleColor.White;
                }
            }

            menu.menuSelect();

        }

    }
}