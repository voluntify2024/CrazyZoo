using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrazyZoo.Animals
{
    public class Dog : Animal, ICrazyAction, ILandAnimal
    {
        public override string Species => "Dog";
        public bool IsLand { get; set; } = true;

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

        public void Walk(Action<string> log)
        {
            IsLand = true;
            log($"{Name} walks excitedly!");
        }

        public override void OnFoodDropped()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow)?.AddCrazyAction($"{Name} runs happily to the food and barks!");
            });
        }

        public Dog(string name, int age) : base(name, age)
        {
            Name = name;
            Age = age;
        }
    }
}
