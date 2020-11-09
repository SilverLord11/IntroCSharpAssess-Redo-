using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessRedo
{
    class Bow : Item
    {
        private int damage;
        private int range;
        private int defense;
        private int healing;

        public int Damage { get => damage; set => damage = value; }
        public int Range { get => range; set => range = value; }
        public int Defense { get => defense; set => defense = value; }
        public int Healing { get => healing; set => healing = value; }

        public override void PrintDetails()
        {
            base.PrintDetails();
            Util.Prompt($"you want to hit your enemies from afar? I don' blame ya!");
            Util.Prompt($"Her damage characteristic is {damage}");
            Util.Prompt($"and she can shoot a distance of up to {range} units");
            Util.Prompt($"She also can protect you {defense} units, and can heal ya up to {healing} units.");
        }
    }
}
