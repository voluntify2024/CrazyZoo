using CrazyZoo.Animals;
using CrazyZoo.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CrazyZoo
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Animal> Animals = new ObservableCollection<Animal>();
        private ObservableCollection<string> CrazyActions = new ObservableCollection<string>();

        private Animal SelectedAnimal => AnimalsListBox.SelectedItem as Animal;

        public MainWindow()
        {
            InitializeComponent();

            Animals.Add(new Parrot { Name = "Kesha", Age = 3 });
            Animals.Add(new Dog { Name = "Lucy", Age = 8 });
            Animals.Add(new Cat { Name = "Nusha", Age = 6 });
            Animals.Add(new Dinosaur { Name = "Terex", Age = 120 });
            Animals.Add(new FlyingSquirrel { Name = "Bob", Age = 2 });
            Animals.Add(new Turtle { Name = "Kevin", Age = 58 });

            AnimalsListBox.ItemsSource = Animals;
            CrazyActionsListBox.ItemsSource = CrazyActions;
        }

        private void AnimalsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AnimalsListBox.SelectedItem is Animal selected)
                DetailsTextBlock.Text = $"{selected.Name} - Age: {selected.Age}, Type: {selected.Species}";
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
                return;

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
                crazy.ActCrazy(Animals, msg => result = msg);
                CrazyActions.Add(result);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameInputTextBox.Text.Trim();
                string ageText = AgeInputTextBox.Text.Trim();
                string type = TypeInputTextBox.Text.Trim();
                string crazy = CrazyActionInputTextBox.Text.Trim(); 

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Please enter a name!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(ageText, out int age))
                {
                    MessageBox.Show("Please enter a valid age!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(type))
                {
                    MessageBox.Show("Please enter a type!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Animal newAnimal;
                switch (type.ToLower())
                {
                    case "cat":
                        newAnimal = new Cat { Name = name, Age = age };
                        break;
                    case "dog":
                        newAnimal = new Dog { Name = name, Age = age };
                        break;
                    case "parrot":
                        newAnimal = new Parrot { Name = name, Age = age };
                        break;
                    default:
                        newAnimal = new CustomAnimal(type)
                        {
                            Name = name,
                            Age = age
                        };
                        break;
                }

                newAnimal.UserCrazyAction = crazy;

                Animals.Add(newAnimal);

                MessageBox.Show(
                    $"{newAnimal.Name} the {newAnimal.Species} was successfully added!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                NameInputTextBox.Text = Resource1.NameInput;
                AgeInputTextBox.Text = Resource1.AgeInput;
                TypeInputTextBox.Text = Resource1.ChoosingType;
                CrazyActionInputTextBox.Text = Resource1.CrazyActionInput;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = SelectedAnimal;
            if (selected == null)
            {
                MessageBox.Show("Please select an animal to delete!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Animals.Remove(selected);

            var actionsToRemove = CrazyActions
                .Where(a => a.StartsWith(selected.Name + " "))
                .ToList();

            foreach (var action in actionsToRemove)
                CrazyActions.Remove(action);

            DetailsTextBlock.Text = Resource1.DetailsText;
            EatingTextBlock.Text = Resource1.EatingText;
            SoundTextBlock.Text = Resource1.SoundText;
        }
    }
}
