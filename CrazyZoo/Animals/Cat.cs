using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Animals
{
    public class Cat : Animal, ICrazyAction
    {
        public override string Species => "Cat";

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} says: Meow!");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            var random = new Random();
            var count = allAnimals.Count();
            var target = allAnimals.ElementAt(random.Next(count));
            log($"{Name} jumps on {target.Name} and starts playing!");
        }
    }
}
