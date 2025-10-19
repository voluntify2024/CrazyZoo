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
    public class Turtle : Animal, ICrazyAction
    {
        public override string MakeSound() => "Taaaaaalk slooooowly!";

        public string ActCrazy()
        {
            return $"{Name} ({Species}) participates in Formula 1 racing🏎️";
        }
    }
}
