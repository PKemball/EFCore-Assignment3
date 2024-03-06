using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Repo<T> : IRepo<T> where T : Entity
    {
        private DbContext context;

        public Repo(DbContext context)
        {
            this.context = context;
        }

        public void CommitChanges()
        {
            context.SaveChanges();
        }

        public virtual T CreateEntity(T entity)
        {
            var dbSet = context.Set<T>();
            dbSet.Add(entity);
            return entity;
        }

        public virtual void DeleteById(int id)
        {
            var dbSet = context.Set<T>();
            //T = entity
            var foundEntity = dbSet.Where(entity => entity.Id == id).First();
            if(foundEntity != null)
            {
                dbSet.Remove(foundEntity);
            }           
        }

        public virtual T FindById(int id)
        {
            var dbSet = context.Set<T>();
            //T = entity
            var foundEntity = dbSet.Where(entity => entity.Id == id).First();
            return foundEntity;
        }

        public virtual List<T> GetAll()
        {
            var dbSet = context.Set<T>();
            return dbSet.ToList();
        }

        public virtual void Update(T entity)
        {
            var dbSet = context.Set<T>();
            var foundEntity = dbSet.Where(entity => entity.Id == entity.Id).First();
            if (foundEntity == null)
            {
                CreateEntity(entity);
                return;
            }

            var foundType = foundEntity.GetType();
            var properties = foundType.GetProperties();
            foreach(var property in properties)
            {
                var value = property.GetValue(entity);
                property.SetValue(foundEntity, value);
            }

            entity.LastUpdatedDateTime = DateTime.Now;
        }
    }
}
