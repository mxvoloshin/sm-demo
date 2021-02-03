using BlazorApp.Api.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Api.Repository
{
    public interface IBaseRepository<T> where T : IAuditableEntity, new()
    {
        Task<TableResult> CreateAsync(T entity);
        Task<TableResult> DeleteAsync(T entity);
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        Task<TableResult> UpdateAsync(T entity);
        T FindById(string id);
    }
}
