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
            probAll += KillChance(mAttacker.Weapons);

            return probAll;
        }

        public double KillChance(List<Weapon> pWeapons)
        {
            int defenderHealth = mDefender.ModelCount * mDefender.Wounds;
            double probAll = 0;

            for (int i = 0; i < pWeapons.Count; i+= pWeapons[0].Damage)
            {
                for (int j = 0; j < pWeapons.Count; j+= pWeapons[1].Damage)
                {
                    if (i + j == 20)
                    {
                        double probWeapon0doesIdmg = UsefulMethods.BinomialDistribution(pWeapons[0].Attacks * mAttacker.ModelCount, i / pWeapons[0].Damage, ChanceToDamage(pWeapons[0]));
                        double probWeapon1doesJdmg = UsefulMethods.BinomialDistribution(pWeapons[1].Attacks * mAttacker.ModelCount, j / pWeapons[1].Damage, ChanceToDamage(pWeapons[1]));

                        probAll += probWeapon0doesIdmg * probWeapon1doesJdmg ;
                    }

                }
            }

                return probAll;
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
