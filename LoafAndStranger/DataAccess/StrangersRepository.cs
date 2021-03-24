using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace LoafAndStranger.DataAccess
{
    public class StrangersRepository
    {
        AppDbContext _db;

        public StrangersRepository(AppDbContext db)
        {
            _db = db;

        }
        public IEnumerable<Stranger> GetAll()
        {
                var strangers = _db.Strangers
                .Include(s => s.Loaf)
                .Include(s => s.Top);

            return strangers;
        }
    }
}
