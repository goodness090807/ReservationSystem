using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repository
{
    public class BaseRepository
    {
        private readonly string _ConnectionString;
        public BaseRepository(IConfiguration config)
        {
            _ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        public int Execute(string sql, object param = null)
        {
            int affectedRows = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(_ConnectionString))
            {
                connection.Open();
                affectedRows = connection.Execute(sql, param);
            }

            return affectedRows;

        }

        public T Query<T>(string sql, object param = null)
        {
            T retrunT;

            using (NpgsqlConnection connection = new NpgsqlConnection(_ConnectionString))
            {
                retrunT = connection.Query<T>(sql, param).FirstOrDefault();
            }

            return retrunT;
        }

        public List<T> QueryList<T>(string sql, object param = null)
        {
            List<T> retrunT;

            using (NpgsqlConnection connection = new NpgsqlConnection(_ConnectionString))
            {
                retrunT = connection.Query<T>(sql, param).ToList();
            }

            return retrunT;
        }
    }
}
