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

        CombatEncounter test = new CombatEncounter(army.Units[0], army.Units[1]);
        double avDmg = test.AverageDamage();
        double killPercent = test.KillChance(test.Attacker.Weapons[0]);
        double allOut = test.KillChance();


        Console.WriteLine(test.Attacker.Name + " would deal " + avDmg + " damage to " + test.Defender.Name);//test this
        Console.WriteLine(test.Attacker.Name + " has a " + killPercent + "% chance to kill " + test.Defender.Name + " with weapon " + test.Attacker.Weapons[0].Name);
        Console.WriteLine(test.Attacker.Name + " has a " + allOut + "% chance to kill " + test.Defender.Name + " with all weapons.");

    }
}
