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
            float killChance = 0;

            int defenderHealth = mDefender.ModelCount * mDefender.Wounds;
            int maxDamage = 0;
            for (int i =0; i < mAttacker.Weapons.Count; i++)
            {
                maxDamage = maxDamage + mAttacker.ModelCount * mAttacker.Weapons[i].Damage * mAttacker.Weapons[i].Attacks;
            }

            if(maxDamage > defenderHealth)
            { return 0; }

            int savesFailed = defenderHealth / mAttacker.Weapons[0].Damage;


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
