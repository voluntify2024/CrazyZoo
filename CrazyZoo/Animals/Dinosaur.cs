using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Animals
{
    public class Dinosaur : Animal, ICrazyAction
    {
        public override string Species => "Dinosaur";

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} roars: RAAAWRRRR!!! 🦖");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            log($"{Name} ({Species}) plays football⚽");
        }
    }
}
