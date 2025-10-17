using CrazyZoo.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Interfaces
{
    public interface ICrazyAction
    {
        void ActCrazy(IEnumerable<Animal> allAnimals, Action<string> log);
    }
}
