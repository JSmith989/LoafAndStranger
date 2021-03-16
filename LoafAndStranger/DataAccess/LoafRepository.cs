using LoafAndStranger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LoafAndStranger.DataAccess
{
    public class LoafRepository
    {
        const string ConnectionString = "Server=localhost;Database=LoafAndStranger;Trusted_Connection=True;";
        static List<Loaf> _loaves = new List<Loaf>
            {
                new Loaf { Id = 1, Price = 5.50m, Size = LoafSize.Medium, Sliced = true, Type = "Rye" },
                new Loaf { Id = 2, Price = 2.50m, Size = LoafSize.Small, Sliced = false, Type = "French" }
            };
        
        public List<Loaf> GetAll()
        {
            var loaves = new List<Loaf>();

            //1 create connection
            using var connection = new SqlConnection(ConnectionString);

            //2 open connection
            connection.Open();

            //3 create a command
            var command = connection.CreateCommand();
            // or
            /*var command = new SqlCommand();
            command.Connection = connection;*/

            //4 telling the command what you want to do
            var sql = @"SELECT *
                        FROM Loaves";
            command.CommandText = sql;

            //5 send command to sql server or "execute the command"
            //nonquery - doesnt care just shows that it works
            //reader - gets all results
            //scalar - left most top column
            var reader = command.ExecuteReader();

            //6 loop over our results
            // while loop continues to pull one row at a time until gone
            while (reader.Read())
            {
                
                // add to lisrt
                loaves.Add(MapLoaf(reader));
            }
            
            return loaves;
            
        }

        public void Add(Loaf loaf)
        {
            var biggestExistingId = _loaves.Max(bread => bread.Id);
            loaf.Id = biggestExistingId + 1;

            _loaves.Add(loaf);
        }

        public Loaf Get(int id)
        {
            var sql = @"SELECT *
                        FROM Loaves
                        WHERE Id = @id"; // this id has to match with below

            // create a connection
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            // create command
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("id", id); // RIGHT HERE IS WHAT MATCHES!

            // execute command
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var loaf = MapLoaf(reader);
                return loaf;
            }

            return null;
           /* var loaf = _loaves.FirstOrDefault(bread => bread.Id == id);
            return loaf;*/
        }

        public void Remove(int id)
        {
            var loafToRemove = Get(id);
            _loaves.Remove(loafToRemove);
        }

        Loaf MapLoaf(SqlDataReader reader)
        {
            var id = (int)reader["Id"]; // using (int) is explicit cast, throws exceptions
            var size = (LoafSize)reader["Size"];
            var type = reader["Type"] as string; // implicit casting, returns null and not exception
            var weightInOunces = (int)reader["WeightInOunces"];
            var price = (decimal)reader["Price"];
            var sliced = (bool)reader["Sliced"];
            var createdDate = (DateTime)reader["CreatedDate"];

            // make a loaf
            var loaf = new Loaf
            {
                Id = id,
                Price = price,
                Size = size,
                Sliced = sliced,
                Type = type,
                WeightInOunces = weightInOunces,
            };
            return loaf;
        }
    }
}
