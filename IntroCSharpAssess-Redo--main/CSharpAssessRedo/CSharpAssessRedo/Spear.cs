using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessRedo
{
    class Spear : Item
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
            Util.Prompt($"These folks aren't really different from the Swords except for their added range, but that means generally less damage.");
            Util.Prompt($"Her damage is {damage}.");
            Util.Prompt($"Her range is {range}.");
            Util.Prompt($"This baby can take a hit of up to {defense} units, and she has a healing factor of {healing} units.");
        }
    }
}
