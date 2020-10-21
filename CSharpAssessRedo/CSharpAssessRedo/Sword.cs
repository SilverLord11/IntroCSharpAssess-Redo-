using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessRedo
{
    class Sword : Item
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
            Util.Prompt($"Think of these guys as a Jack of all trades, master of some");
            Util.Prompt($"It's Damage is {damage}");
            Util.Prompt($"It's rangee is {range}");
            Util.Prompt($"It's defense is {defense}");
            Util.Prompt($"And It's Healing Factor is {healing}");
        }
    }
}
