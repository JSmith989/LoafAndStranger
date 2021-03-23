using LoafAndStranger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LoafAndStranger.DataAccess
{
    public class LoafRepository
    {
        const string ConnectionString = "Server=localhost;Database=LoafAndStranger;Trusted_Connection=True;";
        
        public List<Loaf> GetAll()
        {
            var loaves = new List<Loaf>();

            //1 create connection
            using var db = new SqlConnection(ConnectionString);


            // telling the command what you want to do
            var sql = @"SELECT *
                        FROM Loaves";

            var results = db.Query<Loaf>(sql).ToList();
            
            return results;
            
        }

        public void Add(Loaf loaf)
        {
            var sql = @"INSERT INTO [dbo].[Loaves] ([Size],[Type],[WeightInOunces],[Price],[Sliced])
                                    OUTPUT inserted.Id 
                                    VALUES(@Size ,@Type, @WeightInOunces, @Price, @Sliced)";
            // create connection
            using var db = new SqlConnection(ConnectionString);
            var id = db.ExecuteScalar<int>(sql, loaf);


            loaf.Id = id;
            /*_loaves.Add(loaf);*/
        }

        public Loaf Get(int id)
        {
            var sql = @"SELECT *
                        FROM Loaves
                        WHERE Id = @id"; // this id has to match with below

            // create a connection
            using var db = new SqlConnection(ConnectionString);

            var loaf = db.QueryFirstOrDefault<Loaf>(sql, new { id = id });
            return loaf;
            /*connection.Open();

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

            return null;*/
           /* var loaf = _loaves.FirstOrDefault(bread => bread.Id == id);
            return loaf;*/
        }

        public void Slice(int id)
        {
            var sql = @"UPDATE Loaves
                        SET Sliced = 1
                        WHERE Id = @id";
            using var db = new SqlConnection(ConnectionString);
            db.Execute(sql, new { id });
        }

        public void Remove(int id)
        {
            var sql = @"Delete
                        FROM Loaves
                        WHERE Id = @id";
            using var db = new SqlConnection(ConnectionString);
            db.Execute(sql, new { id }); // this does the same as id = id

        }

      public void Update(Loaf loaf)
        {
            var sql = @"UPDATE Loaves
                            SET Price = @price,
	                        Size = @size,
	                        WeightInOunces = @weightinounces,
	                        Sliced = @sliced,
	                        Type = @type
                        WHERE Id = @id";

            using var db = new SqlConnection(ConnectionString);

            db.Execute(sql, loaf);
        }
    }
}
