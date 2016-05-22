using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unit.Datas.Extensions;
using Util.Datas.EF.Repositories;
using ZPW.Util.Domains;
using ZPW.Util.Domains.Repositories;

namespace Util.Datas.EF {

    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TKey> {

        /// <summary>
        /// 初始化仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected Repository(IUnitOfWork unitOfWork) {
            UnitOfWork = (EFUnitOfWork)unitOfWork;
        }

        /// <summary>
        /// EF工作单元
        /// </summary>
        protected EFUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(IQueryBase<TEntity> query) {
            return FilterBy(Find(), query);
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        protected IQueryable<TEntity> FilterBy(IQueryable<TEntity> queryable, IQueryBase<TEntity> query) {
            var predicate = query.GetPredicate();
            if (predicate == null)
                return queryable;
            return queryable.Where(predicate);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public virtual PagerList<TEntity> PagerQuery(IQueryBase<TEntity> query) {
            return Query(query).PagerResult(query);
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Add(TEntity entity) {
            UnitOfWork.Set<TEntity>().Add(entity);
            UnitOfWork.CommitByStart();
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entities">实体</param>
        public void Add(IEnumerable<TEntity> entities) {
            if (entities == null)
                return;
            UnitOfWork.Set<TEntity>().AddRange(entities);
            UnitOfWork.CommitByStart();
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Update(TEntity entity) {
            UnitOfWork.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            UnitOfWork.CommitByStart();
        }

        public void Remove(TKey id) {
            var entity = Find(id);
            if (entity == null)
                return;
            Remove(entity);
        }

        public void Remove(TEntity entity) {
            UnitOfWork.Set<TEntity>().Remove(entity);
            UnitOfWork.CommitByStart();
        }

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <returns></returns>
        public List<TEntity> FindAll() {
            return Find().ToList();
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Find() {
            return UnitOfWork.Set<TEntity>();
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns></returns>
        public TEntity Find(params object[] id) {
            return UnitOfWork.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">实体标识列表</param>
        /// <returns></returns>
        public List<TEntity> Find(IEnumerable<TKey> ids) {
            if (ids == null) {
                return null;
            }

            return Find().Where(t => ids.Contains(t.Id)).ToList();
        }

        /// <summary>
        /// 索引器查找，获取指定标识的实体
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns></returns>
        public TEntity this[TKey id] {
            get {
                return Find(id);
            }
        }

        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> predicate) {
            return Find().Any(predicate);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save() {
            UnitOfWork.Commit();
        }

        /// <summary>
        /// 获取工作单元
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork GetUnitOfWork() {
            return UnitOfWork;
        }
    }

    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class Repository<TEntity> : Repository<TEntity, Guid> where TEntity : class, IAggregateRoot<Guid> {
        /// <summary>
        /// 初始化仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected Repository(IUnitOfWork unitOfWork)
            : base(unitOfWork) {
        }
    }

}
