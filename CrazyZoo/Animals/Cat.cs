using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;

namespace CrazyZoo.Animals
{
    public class Cat : Animal, ICrazyAction, ILandAnimal
    {
        public override string Species => "Cat";
        public bool IsLand { get; set; } = true;

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

        public void Walk(Action<string> log)
        {
            IsLand = true;
            log($"{Name} walks excitedly!");
        }

        public override void OnFoodDropped()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow)?.AddCrazyAction($"{Name} elegantly walks to the food and sniffs first 😼");
            });
        }

        public Cat(string name, int age) : base(name, age)
        {
            Name = name;
            Age = age;
        }
    }
}
