using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Animals
{
    public class CustomAnimal : Animal
    {
        private string _species;

        public CustomAnimal(string species = "Unknown")
        {
            _species = species;
        }

        public override string Species => _species;

        public override void MakeSound(Action<string> output)
        {
            output($"{Name} makes some sound!");
        }
    }
}
