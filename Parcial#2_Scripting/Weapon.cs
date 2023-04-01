using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2_Scripting
{
    public class Weapon : Equipment
    {
        private static string[] WeaponNames = { "Golden Weapon", "Dark Weapon", "Shiny Weapon" };
        private string WeaponName;
        public static string[] weaponNames { get { return WeaponNames; } }
        public string weaponName { get { return WeaponName; } set { WeaponName = value; } }

        public Weapon(string name, int power, int durability, Class equipmentClass) : base(name, power, durability, equipmentClass)
        {
            Random random = new Random();
            int index = random.Next(WeaponNames.Length);
            WeaponName = WeaponNames[index];
        }


    }
}
