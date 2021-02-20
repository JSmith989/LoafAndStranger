using LoafAndStranger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoafAndStranger.DataAccess
{
    public class LoafRepository
    {
        static List<Loaf> _loaves = new List<Loaf>
            {
                new Loaf { Id = 1, Price = 5.50, Size = LoafSize.Medium, Sliced = true, Type = "Rye" },
                new Loaf { Id = 2, Price = 2.50, Size = LoafSize.Small, Sliced = false, Type = "French" }
            };

        public List<Loaf> GetAll()
        {
            return _loaves;
        }

        public void Add(Loaf loaf)
        {
            var biggestExistingId = _loaves.Max(bread => bread.Id);
            loaf.Id = biggestExistingId + 1;

            _loaves.Add(loaf);
        }

        public Loaf Get(int id)
        {
            var loaf = _loaves.FirstOrDefault(bread => bread.Id == id);
            return loaf;
        }

        public void Remove(int id)
        {
            var loafToRemove = Get(id);
            _loaves.Remove(loafToRemove);
        }
    }
}
