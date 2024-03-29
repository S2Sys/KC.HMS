﻿using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KC.HMS.Core.Abstracts
{

    //services.AddScoped(typeof(IGenricRepository < > ), typeof(GenricRepository < > )); 
    public class EntityGenricRepository<T> : IEntityGenricRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> dbSet;
        string errorMessage = string.Empty;
        public EntityGenricRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }
        public T Get(long id)
        {
            return dbSet.SingleOrDefault(s => s.Id == id);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity ");
            }
            dbSet.Add(entity);
            context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Attach(entity);
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            
            dbSet.Remove(entity);
            context.SaveChanges();
        }
    }

}
