using CrazyZoo.Animals.Interfaces;
using CrazyZoo.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CrazyZoo.Animals
{
    public class FlyingSquirrel : Animal, IFlyable, ICrazyAction
    {
        private bool IsFlying = false;

        public override string MakeSound() => "Ouuuui!";

        public void Fly()
        {
            IsFlying = !IsFlying;
        }

        public string ActCrazy()
        {
            Fly();
            return IsFlying
                ? $"{Name} ({Species}) becomes Batman at night🦇"
                : $"{Name} can't fly";
        }
    }
}
