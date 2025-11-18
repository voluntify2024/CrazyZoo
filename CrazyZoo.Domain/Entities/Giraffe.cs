using CrazyZoo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CrazyZoo.Domain.Entities
{
    public class Giraffe : Animal, ICrazyAction, ILandAnimal
    {
        public override string Species => "Giraffe";
        public bool IsLand { get; set; } = true;

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} makes a gentle hum: Hmmmm... 🦒");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            log($"{Name} ({Species}) spins its long neck around and dances funny!");
        }

        public void Walk(Action<string> log)
        {
            IsLand = true;
            log($"{Name} walks elegantly across the savanna.");
        }

        public override void OnFoodDropped()
        {
            RaiseFoodDropped($"{Name} stretches its neck to eat the tallest leaves!");
        }

        public Giraffe(string name, int age) : base(name, age)
        {
            Name = name;
            Age = age;
        }
    }
}
