using CrazyZoo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrazyZoo.Domain.Entities
{
    public class Dinosaur : Animal, ICrazyAction, ILandAnimal
    {
        public override string Species => "Dinosaur";
        public bool IsLand { get; set; } = true;

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} roars: RAAAWRRRR!!! 🦖");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            log($"{Name} ({Species}) plays football⚽");
        }

        public void Walk(Action<string> log)
        {
            IsLand = true;
            log($"{Name} walks excitedly!");
        }

        public override void OnFoodDropped()
        {
            RaiseFoodDropped($"{Name} roars and destroy everything! 🦖");
        }

        public Dinosaur(string name, int age) : base(name, age)
        {
            Name = name;
            Age = age;
        }
    }
}
