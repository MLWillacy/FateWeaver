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
        public float AverageDamage()
        {
            float averageDamage = 0;

            for (int i = 0; i < mAttacker.Weapons.Count; i++)
            {
                Weapon currentWeapon = mAttacker.Weapons[i];
                int totalAttacks = mAttacker.ModelCount * currentWeapon.Attacks;
                float probabilityToWound = totalAttacks * ((float)(7-currentWeapon.ToHit)/6) * ((float)(7-currentWeapon.ToWound)/6);
                
                int rendedSave = mDefender.Save + currentWeapon.Rend;
                float probabilityToSave;
                if (rendedSave > 6)
                {
                    probabilityToSave = 0;
                }
                else
                {
                    probabilityToSave = ((float)(7 - rendedSave) / 6);
                }

                averageDamage = averageDamage + (probabilityToWound * (1-probabilityToSave)) * currentWeapon.Damage;
            }

            return averageDamage;
        }
    }
}
