using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CrazyZoo.Animals
{
    public class Elephant : Animal, ICrazyAction, ILandAnimal
    {
        public override string Species => "Elephant";
        public bool IsLand { get; set; } = true;

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} trumpets loudly: PHRRRR!!!");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            log($"{Name} ({Species}) splashes everyone with water from its trunk!");
        }

        public void Walk(Action<string> log)
        {
            IsLand = true;
            log($"{Name} walks majestically across the savannah.");
        }

        public override void OnFoodDropped()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow)?.AddCrazyAction(
                    $"{Name} joyfully waves its trunk and eats peanuts!"
                );
            });
        }

        public Elephant(string name, int age) : base(name, age)
        {
            Name = name;
            Age = age;
        }
    }
}
