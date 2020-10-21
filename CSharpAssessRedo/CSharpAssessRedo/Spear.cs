using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessRedo
{
    class Spear : Item
    {
        private int damage;
        private int range;

        public int Damage { get => damage; set => damage = value; }
        public int Range { get => range; set => range = value; }

        public override void PrintDetails()
        {
            base.PrintDetails();
            Util.Prompt($"These folks aren't really different from the Swords except for their added range, no defense and no healing");
            Util.Prompt($"It's Damage is {damage}");
            Util.Prompt($"It's Range is {range}");
        }
    }
}
