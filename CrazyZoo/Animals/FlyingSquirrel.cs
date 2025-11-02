using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;   

namespace CrazyZoo.Animals
{
    public class FlyingSquirrel : Animal, IFlyable, ICrazyAction
    {
        private bool IsFlying = true;

        public override string Species => "Flying Squirrel";

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} says: Ouuuui!");
        }

        public void Fly(Action<string> log)
        {
            IsFlying = !IsFlying;
            log(IsFlying
                ? $"{Name} spreads its arms and glides through the trees! 🦅"
                : $"{Name} lands gracefully.");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            Fly(log);
            log($"{Name} ({Species}) becomes Batman at night 🦇");
        }

        public override void OnFoodDropped()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow)?.AddCrazyAction($"{Name} grabs and flies away");
            });
        }

        public FlyingSquirrel(string name, int age) : base(name, age)
        {
            Name = name;
            Age = age;
        }
    }
}
