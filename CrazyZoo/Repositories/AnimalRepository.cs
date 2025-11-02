using CrazyZoo.Animals;
using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Repositories
{
    public class AnimalRepository : IRepository<Animal>
    {
        public ObservableCollection<Animal> Animals { get; } = new ObservableCollection<Animal>();

        public void Add(Animal item)
        {
            Animals.Add(item);
        }

        public void Remove(Animal item)
        {
            Animals.Remove(item);
        }

        public IEnumerable<Animal> GetAll()
        {
            return Animals;
        }

        public Animal Find(Func<Animal, bool> predicate)
        {
            return Animals.FirstOrDefault(predicate);
        }
    }
}
