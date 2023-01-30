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
            mModelCount = int.Parse(pUnitStatSheet["C" + unitLocation].ToString());
            mBaseSize = int.Parse(pUnitStatSheet["D" + unitLocation].ToString());

            mWounds = int.Parse(pUnitStatSheet["E" + unitLocation].ToString());
            mMovement = int.Parse(pUnitStatSheet["F" + unitLocation].ToString());
            mBravery = int.Parse(pUnitStatSheet["G" + unitLocation].ToString());
            mSave = int.Parse(pUnitStatSheet["H" + unitLocation].ToString());

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

                char lastChar = currentColumn.Last();
                if (lastChar == 'Z')
                {
                    
                    string newColumn = "";
                    for (int j = currentColumn.Length - 1; j > -1; j--)
                    {
                        if (currentColumn[j] == 'Z')
                        {                            
                            newColumn = currentColumn.Substring(0, j) + 'A';
                            if (j == 0)
                            {
                                newColumn = "A" + newColumn;
                                currentColumn = newColumn;
                                break;
                            }
                        }
                        else
                        {
                            char nextChar = (char)(((int)currentColumn[j]) + 1);
                            newColumn = currentColumn.Substring(0, j) + nextChar.ToString() + currentColumn.Substring(j + 1, currentColumn.Length - 1);
                            break;
                        }
                        currentColumn = newColumn;
                    }
                }
                else
                {
                    char nextChar = (char)(((int)lastChar) + 1);
                    currentColumn = currentColumn.Substring(0,currentColumn.Length - 1) + nextChar.ToString();
                }
            }
        }

        public string Name { get { return mName; } }
        public int PointCost { get { return mPointCost; } }
        public int ModelCount { get { return mModelCount; } }
        public int BaseSize { get { return mBaseSize; } }
        public string[] Keywords { get { return mKeywords; } }
        public List<Weapon> Weapons { get { return mWeapons;} }
        public int Wounds { get { return mWounds; } }
        public int Movement { get { return mMovement; } }
        public int Bravery { get { return mBravery; } }
        public int Save { get { return mSave; } }


    }


}
