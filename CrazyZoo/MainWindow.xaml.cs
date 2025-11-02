using CrazyZoo.Animals;
using CrazyZoo.Interfaces;
using CrazyZoo.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrazyZoo
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> CrazyActions = new ObservableCollection<string>();

        private readonly AnimalRepository _repo = new AnimalRepository();
        private readonly EnclosureManager _enclosureManager = new EnclosureManager();

        private Animal SelectedAnimal
        {
            get
            {
                if (LandAnimalsListBox.SelectedItem is Animal landAnimal)
                    return landAnimal;
                if (AirAnimalsListBox.SelectedItem is Animal airAnimal)
                    return airAnimal;
                if (WaterAnimalsListBox.SelectedItem is Animal waterAnimal)
                    return waterAnimal;
                return null;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _enclosureManager;
            CrazyActionsListBox.ItemsSource = CrazyActions;

            LandAnimalsListBox.ItemsSource = _enclosureManager.LandEnclosure.Animals;
            AirAnimalsListBox.ItemsSource = _enclosureManager.AirEnclosure.Animals;
            WaterAnimalsListBox.ItemsSource = _enclosureManager.WaterEnclosure.Animals;
            CrazyActionsListBox.ItemsSource = CrazyActions;

            var parrot = new Parrot("Kesha", 3);
            var dog = new Dog("Lucy", 8);
            var cat = new Cat("Nusha", 6);
            var dinosaur = new Dinosaur("Terex", 120);
            var flyingSquirrel = new FlyingSquirrel("Bob", 2);
            var turtle = new Turtle("Kevin", 58);
            var giraffe = new Giraffe("Longneck", 15);
            var penguin = new Penguin("Waddles", 4);
            var elephant = new Elephant("Dumbo", 25);

            _repo.Add(parrot);
            _repo.Add(dog);
            _repo.Add(cat);
            _repo.Add(dinosaur);
            _repo.Add(flyingSquirrel);
            _repo.Add(turtle);
            _repo.Add(giraffe);
            _repo.Add(penguin);
            _repo.Add(elephant);

            _enclosureManager.AddAnimal(parrot);
            _enclosureManager.AddAnimal(dog);
            _enclosureManager.AddAnimal(cat);
            _enclosureManager.AddAnimal(dinosaur);
            _enclosureManager.AddAnimal(flyingSquirrel);
            _enclosureManager.AddAnimal(turtle);
            _enclosureManager.AddAnimal(giraffe);
            _enclosureManager.AddAnimal(penguin);
            _enclosureManager.AddAnimal(elephant);

            _enclosureManager.LandEnclosure.AnimalJoined += a => CrazyActions.Add($"{a.Name} joined land enclosure");
            _enclosureManager.WaterEnclosure.AnimalJoined += a => CrazyActions.Add($"{a.Name} joined water enclosure");
            _enclosureManager.AirEnclosure.AnimalJoined += a => CrazyActions.Add($"{a.Name} joined flying enclosure");

            _enclosureManager.LandEnclosure.FoodDropped += () => CrazyActions.Add("Food dropped in land enclosure — animals are eating!");
            _enclosureManager.WaterEnclosure.FoodDropped += () => CrazyActions.Add("Food dropped in water enclosure — animals are eating!");
            _enclosureManager.AirEnclosure.FoodDropped += () => CrazyActions.Add("Food dropped in flying enclosure — animals are eating!");
        }

        public void RefreshAnimalList()
        {
            Dispatcher.Invoke(() =>
            {
                LandAnimalsListBox.ItemsSource = null;
                LandAnimalsListBox.ItemsSource = _enclosureManager.LandEnclosure.Animals;

                AirAnimalsListBox.ItemsSource = null;
                AirAnimalsListBox.ItemsSource = _enclosureManager.AirEnclosure.Animals;

                WaterAnimalsListBox.ItemsSource = null;
                WaterAnimalsListBox.ItemsSource = _enclosureManager.WaterEnclosure.Animals;
            });
        }

        public void AddCrazyAction(string message)
        {
            Dispatcher.Invoke(() => CrazyActions.Add(message));
        }

        private void EnclosureListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedAnimal != null)
                DetailsTextBlock.Text = $"{SelectedAnimal.Name} - Age: {SelectedAnimal.Age}, Type: {SelectedAnimal.Species}";
            else
                DetailsTextBlock.Text = "No details";
        }

        private void SoundButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = SelectedAnimal;
            if (selected == null)
            {
                SoundTextBlock.Text = "Please select an animal first!";
                return;
            }

            selected.MakeSound(msg => SoundTextBlock.Text = msg);
        }

        private void FeedButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = SelectedAnimal;
            if (selected == null)
            {
                EatingTextBlock.Text = "Please select an animal first!";
                return;
            }

            string food = InputFood.Text;
            if (string.IsNullOrWhiteSpace(food))
            {
                EatingTextBlock.Text = "Enter some food!";
                return;
            }

            EatingTextBlock.Text = $"{selected.Name} eats {food}";
            InputFood.Clear();
        }

        private void CrazyButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = SelectedAnimal;
            if (selected == null)
            {
                MessageBox.Show("Please select an animal first!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(selected.UserCrazyAction))
            {
                CrazyActions.Add($"{selected.Name} {selected.UserCrazyAction}");
                return;
            }

            if (selected is ICrazyAction crazy)
            {
                string result = "";
                crazy.ActCrazy(_repo.GetAll(), msg => result = msg);
                CrazyActions.Add(result);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
           AddAnimalWindow addWindow = new AddAnimalWindow(_repo, _enclosureManager);
            addWindow.Owner = this;
            addWindow.ShowDialog();

            RefreshAnimalList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = SelectedAnimal;
            if (selected == null)
            {
                MessageBox.Show("Please select an animal to delete!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _repo.Remove(selected);

            _enclosureManager.RemoveAnimal(selected);

            CrazyActions.Add($"{selected.Name} has been removed from the zoo.");

            DetailsTextBlock.Text = "No details";
            EatingTextBlock.Text = "No feeding yet";
            SoundTextBlock.Text = "No sound yet";
        }



        private async void FeedAllButton_Click(object sender, RoutedEventArgs e)
        {
            CrazyActions.Add("Feeding all animals 🍽️");
            await _enclosureManager.FeedAllAsync();
            CrazyActions.Add("All animals have eaten!");
        }

        private void ShowStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            CrazyActions.Clear();
            CrazyActions.Add("--- Animal Statistics ---");

            var stats = _enclosureManager.GetStatistics();
            foreach (var (type, count, avg) in stats)
            {
                CrazyActions.Add($"{type}: {count} animals (avg age: {avg:F1})");
            }
        }
    }
}
