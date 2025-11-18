using CrazyZoo.Domain.Interfaces;
using CrazyZoo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyZoo.Infrastructure.Repositories
{
    public class AnimalRepository : IRepository<Animal>
    {
        private readonly string _connectionString;
        public ObservableCollection<Animal> Animals { get; } = new ObservableCollection<Animal>();

        public AnimalRepository(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
            LoadFromDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(
                    @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Animals' AND xtype='U')
                      CREATE TABLE Animals (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(100) NOT NULL,
                        Age INT NOT NULL,
                        Species NVARCHAR(100) NOT NULL,
                        UserCrazyAction NVARCHAR(255) NULL
                      );", connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void Add(Animal item)
        {
            Animals.Add(item);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Animals (Name, Age, Species, UserCrazyAction) VALUES (@Name, @Age, @Species, @Action)",
                    connection);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Age", item.Age);
                cmd.Parameters.AddWithValue("@Species", item.Species);
                cmd.Parameters.AddWithValue("@Action", item.UserCrazyAction ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        public void Remove(Animal item)
        {
            Animals.Remove(item);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(
                    "DELETE FROM Animals WHERE Name=@Name AND Age=@Age AND Species=@Species",
                    connection);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Age", item.Age);
                cmd.Parameters.AddWithValue("@Species", item.Species);
                cmd.ExecuteNonQuery();
            }
        }

        public Animal Find(Func<Animal, bool> predicate)
        {
            return Animals.FirstOrDefault(predicate);
        }

        public IEnumerable<Animal> GetAll()
        {
            return Animals;
        }

        private void LoadFromDatabase()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT Name, Age, Species, UserCrazyAction FROM Animals", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        int age = reader.GetInt32(1);
                        string species = reader.GetString(2);
                        string action = reader.IsDBNull(3) ? "" : reader.GetString(3);

                        var animal = new CustomAnimal(species, name, age);
                        animal.UserCrazyAction = action;
                        Animals.Add(animal);
                    }
                }
            }
        }
    }
}