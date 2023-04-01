namespace Parcial2_Scripting
{
    public class Create_CharacterTest
    {
        Random random = new Random();
        public string[] randomName = { "Heimdallr", "Padmavati", "Phoibos", "Melqart", "Indrajit", "Penelope", "Atreus", "Devi", "Maia", "Europe", "Diarmait", "Klytië", "Atum", "Taranis", "Heilyn" };
        public string name;
        public string[] weaponNames = { "Golden Weapon", "Dark Weapon", "Shiny Weapon" };
        public string weaponName;
        public string[] armorNames = { "Golden Armor", "Dark Armor", "Shiny Armor" };
        public string armorName;

        public Character character1;
        public Character character2;
        public Weapon weapon1;
        public Weapon weapon2;
        public Weapon noWeapon;
        public Armor armor1;
        public Armor armor2;
        public Armor noArmor;


        [SetUp]
        public void Setup()
        {

            int randomNum = random.Next(9, 21);
            //Armor Generation
            armorName = armorNames[random.Next(0, armorNames.Length)];
            armor1 = new Armor(armorName, randomNum, randomNum, (Equipment.Class)random.Next(0, 3));
            armorName = armorNames[random.Next(0, armorNames.Length)];
            armor2 = new Armor(armorName, randomNum, randomNum, (Equipment.Class)random.Next(0, 3));
            noArmor = new Armor("No Armor", 1, 100, Equipment.Class.Any);

            //Weapon Generation
            weaponName = weaponNames[random.Next(0, weaponNames.Length)];
            weapon1 = new Weapon(weaponName, randomNum, randomNum, armor1.equipmentClass);
            weaponName = weaponNames[random.Next(0, weaponNames.Length)];
            weapon2 = new Weapon(weaponName, randomNum, randomNum, armor2.equipmentClass);
            noWeapon = new Weapon("No Weapon", 1, 100, Equipment.Class.Any);

            //Characters Generation
            name = randomName[random.Next(0, randomName.Length)];
            character1 = new Character(name, weapon1, armor1, randomNum, randomNum, 1, (Character.ChrClass.Human));
            name = randomName[random.Next(0, randomName.Length)];
            character2 = new Character(name, weapon1, armor1, randomNum, randomNum, 1, (Character.ChrClass.Hybrid));

            //Character 1 Equipment check
            string chrWpn1 = character1.weapon.equipmentClass.ToString();
            string chrArm1 = character1.armor.equipmentClass.ToString();
            string chrCls1 = character1.characterClass.ToString();
            if (chrWpn1 != chrCls1) { if (chrWpn1 == "Any") { character1.weapon = weapon1; } else { character1.weapon = noWeapon; } }
            if (chrArm1 != chrCls1) { if (chrArm1 == "Any") { character1.armor = armor1; } else { character1.armor = noArmor; } }

            //Character 2 Equipment check
            string chrWpn2 = character2.weapon.equipmentClass.ToString();
            string chrArm2 = character2.armor.equipmentClass.ToString();
            string chrCls2 = character2.characterClass.ToString();
            if (chrWpn2 != chrCls2) { if (chrWpn2 == "Any") { character2.weapon = weapon2; } else { character2.weapon = noWeapon; } }
            if (chrArm2 != chrCls2) { if (chrArm2 == "Any") { character2.armor = armor2; } else { character2.armor = noArmor; } }

            character1.CalculateAtk(character1);
            character1.CalculateDef(character1);
            character2.CalculateAtk(character1);
            character2.CalculateDef(character2);

        }

        [Test]
        public void NotNullInstance()
        {
            Assert.IsNotNull(character1);
            Assert.IsNotNull(character2);
        }

        [Test]
        public void NoAtrubitesBelow_0()
        {
            //Character 1 atributes check
            Assert.GreaterOrEqual(character1.atk, 1);
            Assert.GreaterOrEqual(character1.def, 1);
            Assert.GreaterOrEqual(character1.hp, 1);

            //Character 2 atributes check
            Assert.GreaterOrEqual(character2.atk, 1);
            Assert.GreaterOrEqual(character2.def, 1);
            Assert.GreaterOrEqual(character2.hp, 1);
        }

        [Test]
        public void NoDurabiltyBelow_1()
        {
            //Weapons Random Durability check
            Assert.GreaterOrEqual(weapon1.durability, 1);
            Assert.GreaterOrEqual(weapon2.durability, 1);

            //Armors Random Durability check
            Assert.GreaterOrEqual(armor1.durability, 1);
            Assert.GreaterOrEqual(armor2.durability, 1);
        }

        [Test]
        public void DurabiltyLossCheck()
        {
            int chrWpn1_durability = character1.weapon.durability;
            int chrArm1_durability = character1.armor.durability;
            int chrWpn2_durability = character2.weapon.durability;
            int chrArm2_durability = character2.armor.durability;

            character1.Attack(character2);
            character2.Attack(character1);

            if (((chrWpn1_durability == (chrWpn1_durability - 1) && chrArm2_durability == (chrArm2_durability) - (character1.atk / 2)) && (chrWpn2_durability == (chrWpn2_durability - 1) && chrArm1_durability == (chrArm1_durability) - (character2.atk / 2))))
            {
                Assert.Pass();
            }
        }

        [Test]
        public void ClassEquipmentCheck()
        {
            //Character 1 check
            string chrWpn1 = character1.weapon.equipmentClass.ToString();
            string chrArm1 = character1.armor.equipmentClass.ToString();
            string chrCls1 = character1.characterClass.ToString();


            if ((chrWpn1 == chrArm1) && (chrArm1 == chrCls1))
            {
                Assert.Pass();
            }
            else
            {
                if (chrWpn1 != chrCls1)
                {
                    if (chrWpn1 == "Any")
                    {
                        Assert.Pass();
                    }
                }

                if (chrArm1 != chrCls1)
                {
                    if (chrArm1 != "Any")
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        [Test]
        public void NoEquipmentOverload()
        {
            Weapon chr1OldWeapon = character1.weapon;
            Armor chr1OldArmor = character1.armor;

            Weapon chr2OldWeapon = character2.weapon;
            Armor chr2OldArmor = character2.armor;

            character1.GetNewWeapon(character1);
            character2.GetNewWeapon(character2);
            character1.GetNewArmor(character1);
            character2.GetNewArmor(character2);

            Weapon chr1NewWeapon = character1.weapon;
            Armor chr1ONewArmor = character1.armor;

            Weapon chr2NewWeapon = character2.weapon;
            Armor chr2NewArmor = character2.armor;

            if (chr1OldWeapon != chr1NewWeapon) { Assert.Pass(); }
            else if (chr1NewWeapon == chr1OldWeapon) { Assert.Fail(); }

            if (chr2OldWeapon != chr2NewWeapon) { Assert.Pass(); }
            else if (chr2NewWeapon == chr2OldWeapon) { Assert.Fail(); }
        }

        [Test]
        public void ArmorlessDamage()
        {
            character2.armor.durability = 0;
            character2.CalculateDef(character2);
            character1.Attack(character2);

            if (character2.hp <= character2.hp - character1.atk)
            {
                Assert.Fail();
            }
            else if (character2.hp == character2.hp - character1.atk)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void ArmoredDamage()
        {
            character2.CalculateDef(character2);
            character1.Attack(character2);

            if (character2.hp <= character2.hp - character1.atk)
            {
                Assert.Pass();
            }
            else if (character2.hp == character2.hp - character1.atk)
            {
                Assert.Fail();
            }
        }


        [Test]
        public void WeaponlessDamage()
        {
            character2.weapon = noWeapon;
            character2.CalculateAtk(character2);
            character2.Attack(character1);
            if (character1.hp >= character2.atk)
            {
                Assert.Fail();
            }
            else if (character1.hp == character2.atk / 2)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void WeaponlessAndArmorlessDamage()
        {
            character2.weapon = noWeapon;
            character2.CalculateAtk(character2);
            character1.armor = noArmor;
            character1.CalculateDef(character1);
            int chr1OldHp = character1.hp;
            character2.Attack(character1);
            if (chr1OldHp == character1.hp) //jmmm
            {
                Assert.Pass();
            }
            else { Assert.Fail(); }
        }

        [Test]
        public void BrokenWeapon()
        {
            character1.weapon.durability = 1;
            character1.CalculateAtk(character1);
            character1.Attack(character2);
            if (character1.weapon.durability == 1)
            {
                Assert.Fail();
            }
            else { Assert.Pass(); }
        }

        [Test]
        public void BrokenArmor()
        {
            character1.armor.durability = 1;
            character1.CalculateDef(character1);
            character2.Attack(character1);
            character1.CalculateDef(character1);
            if (character1.armor.durability == 1)
            {
                Assert.Fail();
            }
            else { Assert.Pass(); }
        }

        [Test]
        public void NoNegativeHealtPoints()
        {
            character1.CalculateAtk(character1);
            character2.hp = 1;
            character2.CalculateDef(character2);
            character1.Attack(character2);

            if (character2.hp < 0)
            {
                Assert.Fail();
            }
            else { Assert.Pass(); }
        }

        [Test]
        public void NoNegativeDurabilityArmor()
        {
            character1.CalculateAtk(character1);
            character2.armor.durability = 1;
            character2.CalculateDef(character2);
            character1.Attack(character2);

            if (character2.armor.durability < 0) { Assert.Fail(); } else { Assert.Pass(); }
        }

        [Test]
        public void NoNegativeDurabilityWeapon()
        {
            character1.weapon.durability = 1;
            character1.CalculateAtk(character1);
            character2.CalculateDef(character2);
            character1.Attack(character2);

            if (character2.weapon.durability < 0) { Assert.Fail(); } else { Assert.Pass(); }
        }

        [Test]
        public void AlwaysAble2Equip()
        {
            Weapon chr1OldWeapon = character1.weapon;
            Armor chr1OldArmor = character1.armor;

            Weapon chr2OldWeapon = character2.weapon;
            Armor chr2OldArmor = character2.armor;

            character1.GetNewWeapon(character1);
            character1.GetNewArmor(character1);

            Weapon chr1NewWeapon = character1.weapon;
            Armor chr1ONewrmor = character1.armor;

            if (chr1OldWeapon != chr1NewWeapon) { Assert.Pass(); }
            else if (chr1NewWeapon == chr1OldWeapon) { Assert.Fail(); }
        }

        [Test]
        public void NoBrokenEquipment()
        {
            character1.CreateWeaponManually(weaponName, 10, 0, Equipment.Class.Any);
            if (character1.weapon.durability <= 0) { Assert.Fail(); } else { Assert.Pass(); }

            character1.CreateArmorManually(armorName, 10, 0, Equipment.Class.Any);
            if (character1.armor.durability <= 0) { Assert.Fail(); } else { Assert.Pass(); }
        }

        [Test]
        public void NoClassChange()
        {
            string chr1Wpn = character1.weapon.equipmentClass.ToString();
            string chr1Arm = character1.armor.equipmentClass.ToString();
            string chr1class = character1.characterClass.ToString();
            if (chr1class != character1.characterClass.ToString() || chr1Arm != character1.armor.equipmentClass.ToString() || chr1Wpn != character1.weapon.equipmentClass.ToString())
            {
                Assert.Fail();
            }
            else { Assert.Pass(); }
        }

        [Test]
        public void NoEqupmentAtributesChanges()
        {
            Weapon chr1weapon = character1.weapon;
            if (character1.weapon != chr1weapon) { Assert.Fail(); } else { Assert.Pass(); }

            Armor chr1armor = character1.armor;
            if (character1.armor != chr1armor) { Assert.Fail(); } else { Assert.Pass(); }
        }
    }
}