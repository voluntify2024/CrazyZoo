using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Animals
{
    public abstract class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public abstract string Species { get; }
        public string UserCrazyAction { get; set; } = string.Empty;


        public virtual string Describe()
        {
            return $"{Name}, Age: {Age}";
        }

        public abstract void MakeSound(Action<string> output);

        public override string ToString() => Species;
    }
}
