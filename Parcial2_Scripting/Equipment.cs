using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_Scripting
{
    public class Equipment
    {
        Random random = new Random();
        public enum Class { Human, Beast, Hybrid, Any }
        public string name;
        public int power;
        public int durability;
        public Class equipmentClass;

        public Equipment(string name, int power, int durability, Class equipmentClass)
        {
            this.name = name;
            this.power = power;
            this.durability = durability;
            Array values = Enum.GetValues(typeof(Class));
            Class randomClass = (Class)values.GetValue(random.Next(values.Length));
            this.equipmentClass = randomClass;
        }

        //private Class GetClass()
        //{
        //    var values = Enum.GetValues(typeof(Class));
        //    var randomIndex = random.Next(0, values.Length);
        //    return (Class)values.GetValue(randomIndex);
        //}

        protected int GetPower(int power)
        {
            power = random.Next(2, 10);
            return power;
        }

        protected int GetDurability(int durability)
        {
            durability = random.Next(2, 10);
            return durability;
        }

    }
}
