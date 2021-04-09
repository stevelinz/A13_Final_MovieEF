namespace MovieLibraryOO
{
    public class Menu
    {
        public void menuSelect()
        {
            startUp();


            void startUp()
            {
                System.Console.WriteLine("Enter your selection:");
                System.Console.WriteLine("1) Search Movies");
                System.Console.WriteLine("2) Add Movie");
                System.Console.WriteLine("3) Update Movie");
                System.Console.WriteLine("4) Delete Movie");
                System.Console.WriteLine("Enter q to quit");
            }
        }
    }
}