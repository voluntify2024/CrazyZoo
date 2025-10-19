using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Animals
{
    public class Turtle : Animal, ICrazyAction
    {
        public override string Species => "Turtle";

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} says: Taaaaaalk slooooowly...");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            log($"{Name} ({Species}) participates in Formula 1 racing 🏎️");
        }
    }
}
