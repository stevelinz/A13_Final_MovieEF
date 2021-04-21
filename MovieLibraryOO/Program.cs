using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieLibraryOO.Context;

namespace MovieLibraryOO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Login login = new Login();
            login.userConnectionString();
        }

    }

}


