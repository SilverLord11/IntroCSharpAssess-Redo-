using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessRedo
{
    class Bow : Item
    {
        private int damage;
        private int range;

        public int Damage { get => damage; set => damage = value; }
        public int Range { get => range; set => range = value; }

        public override void PrintDetails()
        {
            base.PrintDetails();
            Util.Prompt($"It's Damage is {damage}");
            Util.Prompt($"and it can shoot a distance of up to {range} units");
        }
    }
}
