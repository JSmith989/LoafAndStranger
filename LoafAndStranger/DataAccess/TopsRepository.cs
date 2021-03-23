using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using LoafAndStranger.Models;
using Dapper;

namespace LoafAndStranger.DataAccess
{
    public class TopsRepository
    {
        const string ConnectionString = "Server=localhost;Database=LoafAndStranger;Trusted_Connection=True;";
        public IEnumerable<Top> GetAll()
        {
            using var db = new SqlConnection(ConnectionString);

            /*var topsSql = @"SELECT * FROM Tops";*/
            /* var strangersSql = "SELECT * FROM Strangers WHERE topId = @id";

             var tops = db.Query<Top>(topsSql);

             foreach (var top in tops)
             {
                 var relatedStrangers = db.Query<Stranger>(strangersSql, top);
                 top.Strangers = relatedStrangers.ToList();
             }*/

            var topsSql = @"SELECT * FROM Tops";
            var strangersSql = "SELECT * FROM Strangers WHERE topId IS NOT NULL";

            var tops = db.Query<Top>(topsSql);
            var strangers = db.Query<Stranger>(strangersSql);

            // same as above
            foreach (var top in tops)
            {
                top.Strangers = strangers.Where(s => s.TopId == top.Id).ToList();
            }

            /*var groupedStrangers = strangers.GroupBy(s => s.TopId);
            // another way to do it
            foreach (var groupedStranger in groupedStrangers)
            {
                tops.First(tops => tops.Id == groupedStranger.Key).Strangers = groupedStranger.ToList();
            }*/

            return tops;
        }

        public Top Add(int numberOfSeats)
        {
            using var db = new SqlConnection(ConnectionString);

            var sql = @"INSERT INTO [Tops] ([NumberOfSeats])
                        Output inserted.*
                        VALUES (@numberOfSeats)";

            var top = db.QuerySingle<Top>(sql, new { numberOfSeats });

            return top;
        }
    }
}
