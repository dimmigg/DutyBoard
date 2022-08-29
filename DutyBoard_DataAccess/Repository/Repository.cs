using Dapper;
using Dapper.Contrib.Extensions;
using DutyBoard_DataAccess.Extensions;
using DutyBoard_DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DutyBoard_DataAccess.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public string Name = typeof(T).Name;
        private int id;
        private readonly string _server;
        private readonly string _db;

        internal SqlConnection GetConnection()
        {
            return new SqlConnection(new SqlConnectionStringBuilder()
            {
                DataSource = _server,
                IntegratedSecurity = true,
                InitialCatalog = _db
            }.ConnectionString);
        }

        public Repository(IConfiguration configuration)
        {
            _server = configuration.GetConnectionString("Server");
            _db = configuration.GetConnectionString("Database");
        }


        public void Add(T entity)
        {
            var props = typeof(T).GetProperties().Where(p => !p.GetCustomAttributes<NotMappedAttribute>().Any() && !p.GetCustomAttributes<KeyAttribute>().Any());
            var dp = new DynamicParameters();
            foreach (var prop in props)
            {
                dp.Add(prop.Name, typeof(T).GetProperty(prop.Name).GetValue(entity, null));
            }
            using (var connect = GetConnection())
            {
                connect.ExecuteProcedure<string>($"tool.uspDutyBoardAdd{Name}", dp);
            }
        }

        public void Update(T entity)
        {
            var props = typeof(T).GetProperties().Where(p => !p.GetCustomAttributes<NotMappedAttribute>().Any());
            var dp = new DynamicParameters();
            foreach (var prop in props)
            {
                dp.Add(prop.Name, typeof(T).GetProperty(prop.Name).GetValue(entity, null));
            }
            using (var connect = GetConnection())
            {
                connect.ExecuteProcedure<string>($"tool.uspDutyBoardEdit{Name}", dp);
            }
        }

        public T FirstOrDefault(Func<T, bool> filter = null)
        {
            using (SqlConnection cn = GetConnection())
            {
                if (filter != null)
                    return cn.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}").FirstOrDefault(filter);
                return cn.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}").FirstOrDefault();
            }
        }

        public IEnumerable<T> GetAll(Func<T, bool> filter = null)
        {
            using (SqlConnection cn = GetConnection())
            {
                if (filter != null)
                    return cn.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}").Where(filter);
                return cn.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}");
            }
        }

        public void Remove(T entity)
        {
            var props = typeof(T).GetProperties().Where(p => p.GetCustomAttributes<KeyAttribute>().Any());
            var dp = new DynamicParameters();
            foreach (var prop in props)
            {
                dp.Add(prop.Name, typeof(T).GetProperty(prop.Name).GetValue(entity, null));
            }
            using (var connect = GetConnection())
            {
                connect.ExecuteProcedure<string>($"tool.uspDutyBoardDel{Name}", dp);
            }
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            foreach (var item in entity)
            {
                Remove(item);
            }
        }

        public void Upsert(T entity)
        {
            var prop = typeof(T).GetProperties().Where(p => p.GetCustomAttributes<KeyAttribute>().Any()).FirstOrDefault();
            var val = typeof(T).GetProperty(prop.Name).GetValue(entity, null).ToString();
            if (int.TryParse(val, out int id) && id == 0)
                Add(entity);
            else
                Update(entity);
        }
    }
}
