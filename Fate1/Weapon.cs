using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Fate1
{
    internal class Weapon
    {
        private string mWeaponType; //either "missile" or "melee"
        private string mWeaponName;
        private int mAttacks;
        private int mRange;
        private int mToHit;
        private int mToWound;
        private int mRend;
        private int mDamage;

        public Weapon(string[] pWeaponStats)
        {
            mWeaponName = pWeaponStats[0];
            mWeaponType = pWeaponStats[1];
            mAttacks = int.Parse(pWeaponStats[3]);
            mRange = int.Parse(pWeaponStats[2]);
            mToHit = int.Parse(pWeaponStats[4]);
            mToWound = int.Parse(pWeaponStats[5]);
            mRend = int.Parse(pWeaponStats[6]);
            mDamage = int.Parse(pWeaponStats[7]);
        }

        public string Name { get { return mWeaponName; } }
        public string Type { get { return mWeaponType; } }
        public int Attacks { get { return mAttacks; } }
        public int Range { get { return mRange;} }
        public int ToHit { get { return mToHit; } }
        public int ToWound { get { return mToWound; } }
        public int Rend { get { return mRend; } }
        public int Damage { get { return mDamage; } }

    }
}
