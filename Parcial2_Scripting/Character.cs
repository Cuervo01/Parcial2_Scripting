using Parcial2_Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_Scripting
{
    public class Character
    {
        public enum ChrClass { Human, Beast, Hybrid }
        Random random = new Random();

        public string[] weaponNames = { "Golden Weapon", "Dark Weapon", "Shiny Weapon" };
        public string weaponName;
        public string[] armorNames = { "Golden Armor", "Dark Armor", "Shiny Armor" };
        public string armorName;

        public string name;
        public Weapon weapon;
        public Armor armor;
        public int hp;
        public int atk;
        public int def;
        public ChrClass characterClass;

        public Character(string name, Weapon weapon, Armor armor, int hp, int atk, int def, ChrClass characterClass)
        {
            this.name = name;
            this.weapon = weapon;
            this.armor = armor;
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.characterClass = characterClass;
        }

        public Character Attack(Character enemy)
        {
            int armDurabilityE = enemy.armor.durability;
            if (armDurabilityE <= 1)
            {
                enemy.hp = enemy.hp - enemy.atk;
                enemy.armor.durability = armDurabilityE - 1;
            }
            else
            {
                enemy.armor.durability = armDurabilityE - (enemy.atk / 2);
            }

            weapon.durability = weapon.durability - 1;

            return enemy;
        }

        public Character CalculateAtk(Character self)
        {
            self.atk = self.atk + self.weapon.power;
            return self;
        }

        public Character CalculateDef(Character self)
        {
            if (self.armor.durability >= 1)
            {
                self.def = self.def + self.armor.power;
            }
            else if (self.armor.durability < 1)
            {
                self.def = self.def - self.armor.power;

            }
            return self;
        }

        public Weapon CreateWeaponManually(string name, int power, int durability, Equipment.Class classtype)
        {
            Weapon newWeapon = new Weapon(name, power, durability, classtype);
            if (newWeapon.durability > 1)
            {
                newWeapon.durability = random.Next(9, 21);
            }
            return newWeapon;
        }

        public Armor CreateArmorManually(string name, int power, int durability, Equipment.Class classtype)
        {
            Armor newArmor = new Armor(name, power, durability, classtype);
            if (newArmor.durability > 1)
            {
                newArmor.durability = random.Next(9, 21);
            }
            return newArmor;
        }

        public Character GetNewWeapon(Character self)
        {
            int randomNum = random.Next(9, 21);
            weaponName = weaponNames[random.Next(0, weaponNames.Length)];
            if (self.weapon != null)
            {
                Weapon newWeapon = new Weapon(weaponName, randomNum, randomNum, (Equipment.Class)random.Next(0, 3));
                if (newWeapon.durability == 0)
                {
                    throw new ArgumentException("You cannot equip a broken weapon");
                }
                else
                {
                    if (self.weapon.equipmentClass.ToString() == self.characterClass.ToString())
                    {
                        self.weapon = self.weapon;
                    }
                    else
                    {
                        self.weapon = null;
                    }
                }
            }
            return self;
        }

        public Character GetNewArmor(Character self)
        {
            int randomNum = random.Next(9, 21);
            armorName = armorNames[random.Next(0, armorNames.Length)];
            if (self.armor != null)
            {
                self.armor = new Armor(armorName, randomNum, randomNum, (Equipment.Class)random.Next(0, 3));
                if (self.armor.equipmentClass.ToString() == self.characterClass.ToString())
                {
                    self.armor = self.armor;
                }
                else
                {
                    self.armor = null;
                }
            }
            return self;
        }
    }
}
