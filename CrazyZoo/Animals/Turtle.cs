using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CrazyZoo.Animals
{
    public class Turtle : Animal, ICrazyAction, ISwimmable
    {
        public override string Species => "Turtle";

        public bool IsSwimming { get; set; } = false;

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} says: Taaaaaalk slooooowly...");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            log($"{Name} ({Species}) participates in Formula 1 racing 🏎️");
        }

        public void Swim(Action<string> log)
        {
            IsSwimming = true;
            log($"{Name} swims!");
        }

        public override void OnFoodDropped()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow)?.AddCrazyAction($"{Name} slowly crawls to the food and... eventually eats it 🐢");
            });
        }

        public Turtle(string name, int age) : base(name, age)
        {
            Name = name;
            Age = age;
        }
    }
}
