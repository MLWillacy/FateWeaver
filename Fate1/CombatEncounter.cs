using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fate1
{
    internal class CombatEncounter
    {
        Unit mAttacker;
        Unit mDefender;

        public CombatEncounter(Unit pAttacker, Unit pDefender)
        {
            mAttacker = pAttacker;
            mDefender = pDefender;
        }

        public Unit Attacker { get { return mAttacker; } }
        public Unit Defender { get { return mDefender; } }
        public double ChanceToDamage(Weapon pWeapon)
        {
            double probabilityToWound = ((double)(7 - pWeapon.ToHit) / 6) * ((double)(7 - pWeapon.ToWound) / 6);

            int rendedSave = mDefender.Save + pWeapon.Rend;
            double probabilityToSave;
            if (rendedSave > 6)
            {
                probabilityToSave = 0;
            }
            else
            {
                probabilityToSave = ((double)(7 - rendedSave) / 6);
            }

            return probabilityToWound * (1 - probabilityToSave);
        }
        public double AverageDamage()
        {
            double averageDamage = 0;

            for (int i = 0; i < mAttacker.Weapons.Count; i++)
            {
                Weapon currentWeapon = mAttacker.Weapons[i];
                int totalAttacks = mAttacker.ModelCount * currentWeapon.Attacks;
                averageDamage = averageDamage + (totalAttacks * ChanceToDamage(currentWeapon) * currentWeapon.Damage);
            }

            return averageDamage;
        }

        public double KillChance()
        {
            double probAll = 0;
            probAll += KillChance(mAttacker.Weapons, new List<int>(),0);

            return probAll;
        }

        public double KillChance(List<Weapon> pWeapons, List<int> pCurrentIterrations, int pWeaponPtr)
        {
            int defenderHealth = mDefender.ModelCount * mDefender.Wounds;
            double probAll = 0;

            if (pCurrentIterrations.Count < pWeapons.Count)
            { pCurrentIterrations.Add(0); }

            int temp = pWeaponPtr;
            for (int i = 0; i < pWeapons[pWeaponPtr].MaxDamage(mAttacker.ModelCount); i+= pWeapons[pWeaponPtr].Damage)
            {
                //pass a list of int? where weapon[0] corresponds to int[0]

                int total = 0;
                foreach (int j in pCurrentIterrations)
                {
                    total += j;
                }

                if (total == defenderHealth)
                {
                    double probThisCombo = 1;
                    for (int k = 0; k < pCurrentIterrations.Count; k++)
                    {
                        probThisCombo = probThisCombo * UsefulMethods.BinomialDistribution(pWeapons[k].Attacks * mAttacker.ModelCount, pCurrentIterrations[k] / pWeapons[k].Damage, ChanceToDamage(pWeapons[k]));
                    }
                    probAll += probThisCombo;
                }
                else if (total < defenderHealth && pWeaponPtr < pWeapons.Count - 1)
                {
                    probAll = probAll * KillChance(pWeapons, pCurrentIterrations, pWeaponPtr + 1);
                }
                pCurrentIterrations[pWeaponPtr]++;

            }

            double killChance = (1 - probAll) * 100;

            pCurrentIterrations[pWeaponPtr] = temp++;

            return killChance;
        }
        public double KillChance(Weapon pWeapon)
        {
            double killChance = 0;

            int defenderHealth = mDefender.ModelCount * mDefender.Wounds;
            int totalAttacks = mAttacker.ModelCount * pWeapon.Attacks;
            int maxDamage = totalAttacks * pWeapon.Damage;
  
            if (maxDamage < defenderHealth)
            { return 0; }

            int minAttacks = defenderHealth / pWeapon.Damage;

            double probToDamage = ChanceToDamage(pWeapon);

            killChance = (1 - UsefulMethods.BinomialDistribution(totalAttacks,minAttacks,probToDamage)) * 100;

            return killChance;
        }
    }
}
