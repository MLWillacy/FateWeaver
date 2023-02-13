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
            sr.Close();

            string path = Path.Combine(Environment.CurrentDirectory, battleTomeName + ".xlsx");

            mBattleTome = WorkBook.Load(path);
            WorkSheet armyStatSheet = mBattleTome.WorkSheets[0];

            for (int i = 0; i < unitNames.Count; i++)
            {
                try 
                {
                        mUnits.Add(new Unit(unitNames[i], armyStatSheet,i));
                }
                catch
                {
                    Console.WriteLine("Unit: " + unitNames[i] + " does not exist or is incorrectly configured in battletome: " + battleTomeName);
                }
            }

            mBattleTome.Close();
        }

        public List<Unit> Units { get { return mUnits; } }
    }
}
