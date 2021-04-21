using MovieLibraryOO.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace MovieLibraryOO.Queries
{
    public class AddUser
    {
       
        public void newUserCreate()
        { 
            var age = "";
            var ageInt = 0;
            var gender = "";
            var zip = "";
            var occ = "";
            System.Console.Write("Create a new User ");
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Wizard");
            Console.ForegroundColor = ConsoleColor.White;

            System.Console.Write("Step 1 of 4: Age?\t");
            andAgain:       
            age = Console.ReadLine();
            if (!Int32.TryParse(age, out ageInt))
            {
                System.Console.Write("\t Enter a number");
                goto andAgain;
            }
            else if(ageInt == 0 || ageInt > 120){
                System.Console.WriteLine("An age between 1 and 120");
                goto andAgain;
            }
           
            System.Console.Write("Step 2 of 4: Gender?\t");

            andTryAgain:       
            gender = Console.ReadLine();
            if (!gender.Equals("M") || !gender.Equals("m") || !gender.Equals("F") || !gender.Equals("f"))
            {
                System.Console.Write("\t Enter either \"M\" or \"F\" ");
                gender = "";
                goto andTryAgain;
            }
            
            System.Console.Write("Step 3 of 4: Zipcode?\t");

            andTryZipAgain:       
            zip = Console.ReadLine();
            if (zip.Length < 5)
            {
                System.Console.Write("\t Zipcodes are at least 5 characters ");
                goto andTryZipAgain;
            }


            using (var db = new MovieContext()) {
                var users = db.Users.Include(x=>x.Occupation)
                                    .Where(x=> x.Id == 1).ToList();
                foreach (var user in users) 
                {
                    System.Console.WriteLine($"User Added: ({user.Id}) {user.Age} {user.Gender} {user.ZipCode} {user.Occupation.Name}");
                }
            }
        }
    }
}