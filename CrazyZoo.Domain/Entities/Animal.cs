using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Domain.Entities
{
    public abstract class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public abstract string Species { get; }
        public string UserCrazyAction { get; set; } = string.Empty;

        protected Animal() { }

        protected Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public event Action<string> FoodDropped;

        protected void RaiseFoodDropped(string message)
        {
            FoodDropped?.Invoke(message);
        }

        public virtual string Describe()
        {
            return $"{Name}, Age: {Age}";
        }

        public abstract void MakeSound(Action<string> output);

        public override string ToString() => Species;

        public virtual void OnFoodDropped()
        {
            Console.WriteLine($"{Name} starts to eat");
        }

        public virtual void OnAnimalJoined(Animal newcomer)
        {
            if (newcomer != this)
                Console.WriteLine($"{Name} noticed that {newcomer.Name} came");
        }
    }
}
