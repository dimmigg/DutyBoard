﻿using Dapper;
using Dapper.Contrib.Extensions;
using DutyBoard_DataAccess.Extensions;
using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Utility.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using DutyBoard_Models;
using static Dapper.SqlMapper;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;

namespace DutyBoard_DataAccess.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public string Name = typeof(T).Name;
        private readonly string _server;
        private readonly string _db;

        internal string GetConnectionString()
        {
            return new SqlConnectionStringBuilder()
            {
                DataSource = _server,
                IntegratedSecurity = true,
                InitialCatalog = _db
            }.ConnectionString;
        }

        internal SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
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
                connect.ExecuteProcedure<object>($"tool.uspDutyBoardAdd{Name}", dp);
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
                connect.ExecuteProcedure<object>($"tool.uspDutyBoardEdit{Name}", dp);
            }
        }

        //public T FirstOrDefault(Func<T, bool> filter = null)
        //{
        //    using (SqlConnection cn = GetConnection())
        //    {
        //        if (filter != null)
        //            return cn.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}").FirstOrDefault(filter);
        //        return cn.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}").FirstOrDefault();
        //    }
        //}

        //public IEnumerable<T> GetAll(Func<T, bool> filter = null)
        //{
        //    using (SqlConnection cn = GetConnection())
        //    {
        //        if (filter != null)
        //            return cn.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}").Where(filter);
        //        return cn.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}");
        //    }
        //}

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
                connect.ExecuteProcedure<object>($"tool.uspDutyBoardDel{Name}", dp);
            }
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            foreach (var item in entity)
            {
                Remove(item);
            }
        }

        public void ClearTable(string table)
        {
            using (var connect = GetConnection())
            {
                connect.Execute($"truncate table {table}");
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

        public void InsertData(IEnumerable<T> data)
        {
            if (!data.Any()) return;

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(GetConnectionString()))
            {
                bulkCopy.DestinationTableName = $"tool.tDutyBoard{Name}";
                foreach (var descriptor in typeof(T).GetProperties().Where(p => !p.GetCustomAttributes<NotMappedAttribute>().Any()))
                {
                    bulkCopy.ColumnMappings.Add(descriptor.Name, descriptor.Name);
                }
                try
                {
                    ClearTable($"tool.tDutyBoard{Name}");
                    bulkCopy.WriteToServer(data.AsTable());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public IEnumerable<T> GetAll(int? id = null)
        {
            var props = typeof(T).GetProperties().Where(p => p.GetCustomAttributes<KeyAttribute>().Any());
            var dp = new DynamicParameters();
            foreach (var prop in props)
            {
                dp.Add(prop.Name, id);
            }
            using (var connect = GetConnection())
            {
                return connect.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}", dp);
            }
        }

        public T FirstOrDefault(int? id = null)
        {
            var props = typeof(T).GetProperties().Where(p => p.GetCustomAttributes<KeyAttribute>().Any());
            var dp = new DynamicParameters();
            foreach (var prop in props)
            {
                dp.Add(prop.Name, id);
            }
            using (var connect = GetConnection())
            {
                return connect.ExecuteProcedure<T>($"tool.uspDutyBoardGet{Name}", dp).FirstOrDefault();
            }
        }
    }
}
