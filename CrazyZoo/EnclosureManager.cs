using CrazyZoo.Animals;
using CrazyZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyZoo
{
    public class EnclosureManager
    {
        public Enclosure<Animal> LandEnclosure { get; } = new Enclosure<Animal> { Name = "Land Animals" };
        public Enclosure<Animal> AirEnclosure { get; } = new Enclosure<Animal> { Name = "Flying Animals" };
        public Enclosure<Animal> WaterEnclosure { get; } = new Enclosure<Animal> { Name = "Water Animals" };

        private readonly List<Enclosure<Animal>> _allEnclosures;

        public EnclosureManager()
        {
            _allEnclosures = new List<Enclosure<Animal>> { LandEnclosure, AirEnclosure, WaterEnclosure };
        }


        public void AddAnimal(Animal animal)
        {
            if (animal == null) return;

            if (animal is IFlyable)
                AirEnclosure.AddAnimal(animal);
            else if (animal.Species.ToLower().Contains("turtle"))
                WaterEnclosure.AddAnimal(animal);
            else
                LandEnclosure.AddAnimal(animal);
        }


        public async Task FeedAllAsync()
        {
            foreach (var enclosure in _allEnclosures)
            {
                Console.WriteLine($"Dropping food in {enclosure.Name}...");
                await enclosure.DropFoodAsync();
            }
        }

        public void RemoveAnimal(Animal animal)
        {
            foreach (var enclosure in _allEnclosures)
            {
                enclosure.RemoveAnimal(animal);
            }
        }

        public IEnumerable<(string Type, int Count, double AverageAge)> GetStatistics()
        {
            return _allEnclosures
                .SelectMany(e => e.Animals)
                .GroupBy(a => a.Species)
                .Select(g => (Type: g.Key, Count: g.Count(), AverageAge: g.Average(a => a.Age)))
                .OrderByDescending(g => g.Count);
        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            return _allEnclosures.SelectMany(e => e.Animals);
        }


    }
}
