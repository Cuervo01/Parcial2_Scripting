using Parcial2_Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_Scripting
{
    public class Armor : Equipment
    {
        private static string[] armorNames = { "Golden Armor", "Dark Armor", "Shiny Armor" };
        private string armorName;
        public static string[] ArmorNames { get { return armorNames; } }
        public string ArmorName { get { return armorName; } set { armorName = value; } }

        public Armor(string name, int power, int durability, Class equipmentClass) : base(name, power, durability, equipmentClass)
        {
            Random random = new Random();
            int index = random.Next(ArmorNames.Length);
            armorName = armorNames[index];
            this.name = armorName;
        }
    }
}
