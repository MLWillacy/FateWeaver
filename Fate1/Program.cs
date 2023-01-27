using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fate1;
using IronXL;

class FateWeaver
{
    static void Main(string[] args)
    {
        string myArmyPath = Path.Combine(Environment.CurrentDirectory, "MyArmy.txt");
        Army army = new Army(myArmyPath);

        // Display the number of command line arguments.
        Console.WriteLine(args.Length);

    }
}
