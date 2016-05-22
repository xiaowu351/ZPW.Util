using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZPW.Util.Domains;
using ZPW.Util.Domains.Repositories;

namespace Util.Datas.EF.Repositories {

    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public interface IRepository<TEntity,in TKey> where TEntity:class,IAggregateRoot<TKey> {

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(IQueryBase<TEntity> query);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PagerList<TEntity> PagerQuery(IQueryBase<TEntity> query);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Add(TEntity entity);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entities">实体</param>
        void Add(IEnumerable<TEntity> entities);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TEntity entity);
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        void Remove(TKey id);
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Remove(TEntity entity);
        /// <summary>
        /// 查找实体集合
        /// </summary>
        List<TEntity> FindAll();
        /// <summary>
        /// 查找实体集合
        /// </summary>
        IQueryable<TEntity> Find();
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        TEntity Find(params object[] id);
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">实体标识列表</param>
        List<TEntity> Find(IEnumerable<TKey> ids);
        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 索引器查找，获取指定标识的实体
        /// </summary>
        /// <param name="id">实体标识</param>
        TEntity this[TKey id] { get; }
        /// <summary>
        /// 保存
        /// </summary>
        void Save();
        /// <summary>
        /// 获取工作单元
        /// </summary>
        IUnitOfWork GetUnitOfWork();
    }

    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity>:IRepository<TEntity,Guid> where TEntity : class, IAggregateRoot<Guid> {

    }
}
