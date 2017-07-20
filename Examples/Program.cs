using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Twizo C# Library testing";
            Console.WriteLine("This application tests the C# library for Twizo.");
            new Menu().RunMenu();
        }
    }
}
