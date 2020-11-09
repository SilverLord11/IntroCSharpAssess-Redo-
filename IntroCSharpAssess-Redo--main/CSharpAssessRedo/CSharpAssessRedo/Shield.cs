using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessRedo
{
    class Shield : Item
    {
        private int damage;
        private int defense;
        private int healing;

        public int Damage { get => damage; set => damage = value; }
        public int Defense { get => defense; set => defense = value; }
        public int Healing { get => healing; set => healing = value; }

        public override void PrintDetails()
        {
            base.PrintDetails();
            Util.Prompt($"Now these guys don't do damage, but don't let that fool ya,");
            Util.Prompt($"because they come with nice pack of armor, this one goes for {defense} units.");
            Util.Prompt($"This baby here cab also heal ya for {healing} units");
        }
    }
}
