using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronXL;

namespace Fate1
{
    internal class Unit
    {
        private string mName;

        private int mPointCost;

        private int mModelCount;
        private int mWounds;
        private int mMovement;
        private int mBravery;
        private int mSave;

        public Unit(string pUnitName,WorkSheet pUnitStatSheet)
        {
            int unitLocation = 0;

            for (int i = 1; i < pUnitStatSheet.Rows.Count(); i++)
            {
                if (pUnitStatSheet["A" + i].ToString().ToLower() == pUnitName.ToLower())
                {
                    unitLocation = i;
                    break;
                }
            }

            mName = pUnitName;
            mPointCost = int.Parse(pUnitStatSheet["B" + unitLocation].ToString());

            Console.WriteLine(mName + " costs " + mPointCost);

        }

    }
}
