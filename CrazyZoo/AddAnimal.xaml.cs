using CrazyZoo.Domain.Entities;
using CrazyZoo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CrazyZoo
{
    public partial class AddAnimalWindow : Window
    {
        private readonly IRepository<Animal> _repo;
        private readonly EnclosureManager _enclosureManager;
        private readonly ILogger _logger;

        public AddAnimalWindow(IRepository<Animal> repo, EnclosureManager manager, ILogger logger)
        {
            InitializeComponent();
            _repo = repo;
            _enclosureManager = manager;
            _logger = logger;

            TypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            string crazy = CrazyActionTextBox.Text.Trim();

            if (!int.TryParse(AgeTextBox.Text.Trim(), out int age) || age < 0)
            {
                MessageBox.Show("Enter a valid age!");
                return;
            }

            if (string.IsNullOrWhiteSpace(name) || TypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Fill in all fields!");
                return;
            }

            AnimalType type = (AnimalType)TypeComboBox.SelectedItem;
            Animal animal = CreateAnimal(type, name, age);
            animal.UserCrazyAction = crazy;

            _repo.Add(animal);
            _enclosureManager.AddAnimal(animal);

            _logger.Log($"Added animal: {animal.Name}, type: {animal.Species}");
            MessageBox.Show($"{animal.Name} successfully added!");

            DialogResult = true;
            Close();
        }

        private Animal CreateAnimal(AnimalType type, string name, int age)
        {
            switch (type)
            {
                case AnimalType.Cat:
                    return new Cat(name, age);
                case AnimalType.Dog:
                    return new Dog(name, age);
                case AnimalType.Parrot:
                    return new Parrot(name, age);
                case AnimalType.Turtle:
                    return new Turtle(name, age);
                case AnimalType.Dinosaur:
                    return new Dinosaur(name, age);
                case AnimalType.FlyingSquirrel:
                    return new FlyingSquirrel(name, age);
                case AnimalType.Giraffe:
                    return new Giraffe(name, age);
                case AnimalType.Penguin:
                    return new Penguin(name, age);
                case AnimalType.Elephant:
                    return new Elephant(name, age);
                default:
                    return new CustomAnimal(type.ToString(), name, age);
            }
        }
    }
}

