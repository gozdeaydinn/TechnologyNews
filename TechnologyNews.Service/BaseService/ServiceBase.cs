﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Entity;
using TechnologyNews.Core.Service;
using TechnologyNews.DAL.Context;

namespace TechnologyNews.Service.BaseService
{
    public class ServiceBase<T>:ICoreService<T> where T:CoreEntity
    {
        private static ProjectContext _context;

        public ProjectContext context//Singleton nesnemizi oluşturduk
        {
            get
            {
                if (_context==null)
                {
                    _context = new ProjectContext();
                    return _context;
                }
                return _context;
            }
            set { _context = value; }
        }
        public int Save()
        {
           return context.SaveChanges();
        }
        public void Add(T item)
        {
            context.Set<T>().Add(item);
            Save();
        }
        public void Add(List<T> items)
        {
            context.Set<T>().AddRange(items);
        }

        public bool Any(Expression<Func<T, bool>> exp) => context.Set<T>().Any(exp);
        public T GetById(Guid id)
        {
           return context.Set<T>().Find(id);
        }

        public List<T> GetActive()
        {
           return context.Set<T>().Where(x => x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated).ToList();
        }
        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public List<T> GetDefault(Expression<Func<T,bool>> exp)
        {
           return context.Set<T>().Where(exp).ToList();
        }
        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Where(exp).FirstOrDefault();
        }

        public void Update(T item)
        {
            T update = GetById(item.ID);
            DbEntityEntry entry = context.Entry(update);
            entry.CurrentValues.SetValues(item);
            Save();
        }

        public void Remove(Guid id)
        {
            T item = GetById(id);
            item.Status = Core.Enum.Status.Deleted;
            Update(item);
        }
        public void Remove(T item)
        {
            item.Status = Core.Enum.Status.Deleted;
            Update(item);
        }
        public void RemoveAll(Expression<Func<T,bool>> exp)
        {
            foreach (var item in GetDefault(exp))
            {
                item.Status = Core.Enum.Status.Deleted;
                Update(item);
            }
        }

    }
}
