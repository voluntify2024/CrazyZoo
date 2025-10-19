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
    public class Dinsaur : Animal, ICrazyAction
    {
        public override string MakeSound() => "Rawrawrawraw!";

        public string ActCrazy()
        {
            return $"{Name} ({Species}) plays football⚽";
        }
    }
}
