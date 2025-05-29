using System;
using System.Linq.Expressions;

namespace Asp2025_DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    T Find(int id);

    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = null, bool isTraking = true);

    T FirstOrDefault(Expression<Func<T, bool>> filter = null,
        string includeProperties = null, bool isTraking = true);

    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);
    void Save();
}
