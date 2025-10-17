using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Animals
{
    public class Parrot : Animal, ICrazyAction, IFlyable
    {
        public override string Species => "Parrot";

        public bool IsFlying { get; set; } = false;

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} says: Squawk!");
        }

        public void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log)
        {
            Fly(log);

            var animalList = allAnimals.ToList();
            if (animalList.Count <= 1)
                return; 

            var random = new Random();

            Animal randomAnimal;
            do
            {
                randomAnimal = animalList[random.Next(animalList.Count)];
            } while (randomAnimal == this);

            string sound = string.Empty;
            randomAnimal.MakeSound(s => sound = s);

            log($"{Name} repeats {randomAnimal.Name}'s sound: {sound}");
        }

        public void Fly(Action<string> log)
        {
            IsFlying = true;
            log($"{Name} flies around excitedly!");
        }
    }
 }
