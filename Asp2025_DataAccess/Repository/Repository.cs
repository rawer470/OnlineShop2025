using System;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using Asp2025_DataAccess.Data;
using Asp2025_DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Asp2025_DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        this._db = db;
       this.dbSet = _db.Set<T>();
    }
    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Find(int id)
    {
        return dbSet.Find(id);
    }

    public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTraking = true)
    {
        IQueryable<T> query = dbSet;

        //Если фильтр не null
        if (filter != null)
        {
            query = query.Where(filter);
        }

        //Если у нас есть свойства для включения
        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        //Если у нас есть отслеживание
        if (!isTraking)
        {
            query = query.AsNoTracking();
        }

        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = null, bool isTraking = true)
    {
        IQueryable<T> query = dbSet;

        //Если фильтр не null
        if (filter != null)
        {
            query = query.Where(filter);
        }

        //Если у нас есть свойства для включения
        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        //Если у нас есть сортировка
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        //Если у нас есть отслеживание
        if (!isTraking)
        {
            query = query.AsNoTracking();
        }

        return query.ToList();


    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
