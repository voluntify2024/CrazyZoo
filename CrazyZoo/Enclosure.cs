using CrazyZoo.Domain.Entities;
using CrazyZoo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrazyZoo
{
    public class Enclosure<T> where T : Animal
    {
            public string Name { get; set; }
            public ObservableCollection<T> Animals { get; } = new ObservableCollection<T>();

            public event Action<T> AnimalJoined;

            public event Action FoodDropped;

            private static readonly Random _random = new Random();

            public void AddAnimal(T animal)
            {
                if (animal == null)
                    throw new ArgumentNullException(nameof(animal));

                if (Animals.Contains(animal))
                    return;

                Animals.Add(animal);
                AnimalJoined?.Invoke(animal);

                FoodDropped += animal.OnFoodDropped;
                AnimalJoined += animal.OnAnimalJoined;
            }

            public void RemoveAnimal(T animal)
            {
                if (animal == null || !Animals.Contains(animal))
                    return;

                Animals.Remove(animal);

                FoodDropped -= animal.OnFoodDropped;
                AnimalJoined -= animal.OnAnimalJoined;
            }

            public async Task DropFoodAsync()
            {
            FoodDropped?.Invoke();

            foreach (var animal in Animals)
            {
                int eatingTime = _random.Next(1000, 3000);
                await Task.Delay(eatingTime);

                string message = $"{animal.Name} ate in {eatingTime / 1000.0:F1} sec";

                Application.Current.Dispatcher.Invoke(() =>
                {
                    (Application.Current.MainWindow as MainWindow)?.AddCrazyAction(message);
                });
            }
        }

            public T Find(Func<T, bool> predicate)
            {
                return Animals.FirstOrDefault(predicate);
            }
        }
    }
