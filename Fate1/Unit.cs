using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronXL;
using SixLabors.ImageSharp.Formats.Tiff.Constants;

namespace Fate1
{
    internal class Unit
    {
        private int mUnique;

        private string mName;
        private int mPointCost;
        private int mModelCount;
        private int mBaseSize;
        private string[] mKeywords;
        private List<Weapon> mWeapons;

        private int mWounds;
        private int mMovement;
        private int mBravery;
        private int mSave;

        private int mHealth;

        public Unit(string pUnitName,WorkSheet pUnitStatSheet, int pUnique)
        {
            mUnique = pUnique;

            int unitLocation = 0;
            int timesReinforced = 1;

            mName = pUnitName;
            if (mName.Contains('*'))
            {
                pUnitName = pUnitName.Substring(0, pUnitName.Length - 1);
                timesReinforced++;
                if (mName.Contains("**"))
                {
                    timesReinforced++;
                    pUnitName = pUnitName.Substring(0, pUnitName.Length - 1);
                }
            }

            for (int i = 1; i < pUnitStatSheet.Rows.Count(); i++)
            {
                if (pUnitStatSheet["A" + i].ToString().ToLower() == pUnitName.ToLower())
                {
                    unitLocation = i;
                    break;
                }
            }

            mPointCost = int.Parse(pUnitStatSheet["B" + unitLocation].ToString());
            mModelCount = int.Parse(pUnitStatSheet["C" + unitLocation].ToString()) * timesReinforced;
            mBaseSize = int.Parse(pUnitStatSheet["D" + unitLocation].ToString());

            mWounds = int.Parse(pUnitStatSheet["E" + unitLocation].ToString());
            mMovement = int.Parse(pUnitStatSheet["F" + unitLocation].ToString());
            mBravery = int.Parse(pUnitStatSheet["G" + unitLocation].ToString());
            mSave = int.Parse(pUnitStatSheet["H" + unitLocation].ToString());

            mHealth = mWounds * mModelCount;

            string keywordsInputString = pUnitStatSheet["I" + unitLocation].ToString();
            mKeywords = keywordsInputString.Split(',');

            int weaponCount = int.Parse(pUnitStatSheet["J" + unitLocation].ToString());
            mWeapons = new List<Weapon>();

            string currentColumn = "K";
            string[] weaponStats = new string[8];
            int arrayPtr = 0;
            for (int i = 0; i < weaponCount*8; i++)
            {
                weaponStats[arrayPtr] = pUnitStatSheet[currentColumn + unitLocation].ToString();
                arrayPtr++;

                if (arrayPtr == 8)
                {
                    mWeapons.Add(new Weapon(weaponStats));
                    arrayPtr = 0;
                }

                currentColumn = pUnitStatSheet.NextCell(currentColumn);
            }
        }

        public int Unique { get { return mUnique; } }
        public string Name { get { return mName; } }
        public int PointCost { get { return mPointCost; } }
        public int ModelCount { get { return mModelCount; } set { mModelCount = value; } }
        public int Health { get { return mHealth; } set { mHealth = value; } }
        public int BaseSize { get { return mBaseSize; } }
        public string[] Keywords { get { return mKeywords; } }
        public List<Weapon> Weapons { get { return mWeapons;} }
        public int Wounds { get { return mWounds; } }
        public int Movement { get { return mMovement; } }
        public int Bravery { get { return mBravery; } }
        public int Save { get { return mSave; } }


    }


}
