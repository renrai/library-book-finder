using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallengeData.Database.Repositories.IRepositories
{
    public interface IRepositoryBase<MODEL> where MODEL : class
    {
        void Add(MODEL model);
        void Update(MODEL model);
        void Remove(MODEL model);
        Task<MODEL> GetById(Guid id);
        Task<IEnumerable<MODEL>> GetAll();

        Task<IEnumerable<MODEL>> GetWherePaged(int skip, int take, Expression<Func<MODEL, bool>> predicate, Expression<Func<IQueryable<MODEL>, IIncludableQueryable<MODEL, object>>> include = null);
        Task<MODEL> FirstOrDefault(Expression<Func<MODEL, bool>> predicate, Expression<Func<IQueryable<MODEL>, IIncludableQueryable<MODEL, object>>> include = null);
    }
}
