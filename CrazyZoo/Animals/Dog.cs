using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Animals
{
    public class Dog : Animal, ICrazyAction
    {
        public override string Species => "Dog";

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} says: Woof-woof!");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            var cats = allAnimals.OfType<Cat>().ToList();
            if (cats.Count == 0)
            {
                log($"{Name} wanted to chase a cat, but there are no cats around :(");
                return;
            }

            var random = new Random();
            var randomCat = cats[random.Next(cats.Count)];

            log($"{Name} starts chasing {randomCat.Name} around excitedly!");
        }
    }
}
