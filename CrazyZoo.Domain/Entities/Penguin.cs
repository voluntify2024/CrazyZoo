using CrazyZoo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CrazyZoo.Domain.Entities
{
    public class Penguin : Animal, ICrazyAction, ISwimmable, ILandAnimal
    {
        public override string Species => "Penguin";

        public bool IsLand { get; set; } = true;
        public bool IsInWater { get; set; } = false;

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} squawks happily: 🐧");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            log($"{Name} ({Species}) slides on its belly across the ice!");
        }

        public void Swim(Action<string> log)
        {
            IsInWater = true;
            log($"{Name} dives gracefully into the icy water!");
        }

        public void Walk(Action<string> log)
        {
            IsLand = true;
            log($"{Name} waddles awkwardly on the snow.");
        }

        public override void OnFoodDropped()
        {
            RaiseFoodDropped($"{Name} catches a fish and eats it greedily!");
        }

        public Penguin(string name, int age) : base(name, age)
        {
            Name = name;
            Age = age;
        }
    }
}
