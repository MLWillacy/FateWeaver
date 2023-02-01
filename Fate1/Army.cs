using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronXL;
using System.IO;

namespace Fate1
{
    internal class Army
    {
        List<Unit> mUnits;
        WorkBook mBattleTome;

        public Army(string pFilePath)
        {
            mUnits = new List<Unit>();

            StreamReader sr = new StreamReader(pFilePath);

            string battleTomeName = sr.ReadLine();

            List<string> unitNames = new List<string>();
            string lineOfText;
            while ((lineOfText = sr.ReadLine()) != null)
            {
                unitNames.Add(lineOfText);
            }

            //need to deal with reinforced units

            string path = Path.Combine(Environment.CurrentDirectory, battleTomeName + ".xlsx");

            mBattleTome = WorkBook.Load(path);
            WorkSheet armyStatSheet = mBattleTome.WorkSheets[0];

            foreach(string unitName in unitNames)
            {
                try 
                {
                        mUnits.Add(new Unit(unitName, armyStatSheet));
                }
                catch
                {
                    Console.WriteLine("Unit: " + unitName + " does not exist or is incorrectly configured in battletome: " + battleTomeName);
                }
            }
        }

        public List<Unit> Units { get { return mUnits; } }
    }
}
