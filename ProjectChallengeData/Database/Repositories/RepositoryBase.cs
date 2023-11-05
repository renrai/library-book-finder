using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProjectChallengeData.Database.Entities;
using ProjectChallengeData.Database.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChallengeData.Database.Repositories
{
    public class RepositoryBase<MODEL, ENTITY> : IRepositoryBase<MODEL>
        where MODEL : class
        where ENTITY : BaseEntity
    {
        #region Fields

        protected readonly ProjectContextDb _db;
        protected readonly IMapper _mapper;

        #endregion

        public RepositoryBase(ProjectContextDb context)
        {
            _db = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(Mapping.MappingProfile));
                cfg.DisableConstructorMapping();
                cfg.AddExpressionMapping();

            });
            _mapper = config.CreateMapper();
        }

        #region Public Methods

        public void Add(MODEL model)
        {
            var entity = _mapper.Map<ENTITY>(model);
            _db.Entry(entity).State = EntityState.Added;
        }

        public void Update(MODEL model)
        {
            var entity = _mapper.Map<ENTITY>(model);
            _db.Entry(entity).State = EntityState.Modified;

        }

        public void Remove(MODEL model)
        {
            var entity = _mapper.Map<ENTITY>(model);
            _db.Entry(entity).State = EntityState.Deleted;
            
        }
        public async Task<MODEL> GetById(Guid id)
        {
            var entity = await _db.Set<ENTITY>().FindAsync(id);
            if (entity != null)
            {
                _db.Entry(entity).State = EntityState.Detached;
            }

            return _mapper.Map<MODEL>(entity);
        }
        public async Task<IEnumerable<MODEL>> GetAll()
        {
            var entities = await _db.Set<ENTITY>().ToListAsync();

            return _mapper.Map<List<MODEL>>(entities);
        }
        public async Task<MODEL> FirstOrDefault(Expression<Func<MODEL, bool>> predicate, Expression<Func<IQueryable<MODEL>, IIncludableQueryable<MODEL, object>>> include = null)
        {
            var query = _db.Set<ENTITY>().AsQueryable().AsNoTracking();
            var whereEntity = _mapper.Map<Expression<Func<ENTITY, bool>>>(predicate);
            var includeEntity = _mapper.Map<Expression<Func<IQueryable<ENTITY>, IIncludableQueryable<ENTITY, object>>>>(include);

            if (include != null)
            {
                query = includeEntity.Compile()(query);
            }

            var result = await query.Where(whereEntity).AsNoTracking().FirstOrDefaultAsync();

            return _mapper.Map<MODEL>(result);
        }
        public async Task<IEnumerable<MODEL>> GetWherePaged(int skip, int take, Expression<Func<MODEL, bool>> predicate, Expression<Func<IQueryable<MODEL>, IIncludableQueryable<MODEL, object>>> include = null)
        {
            var query = _db.Set<ENTITY>().AsQueryable().AsNoTracking();
            var whereEntity = _mapper.Map<Expression<Func<ENTITY, bool>>>(predicate);
            var includeEntity = _mapper.Map<Expression<Func<IQueryable<ENTITY>, IIncludableQueryable<ENTITY, object>>>>(include);

            if (include != null)
            {
                query = includeEntity.Compile()(query);
            }

            var result = await query.Where(whereEntity).AsNoTracking().OrderBy(x => x.CreationDate).Skip(skip).Take(take).ToListAsync();

            return _mapper.Map<List<MODEL>>(result);
        }
        #endregion
    }
}
