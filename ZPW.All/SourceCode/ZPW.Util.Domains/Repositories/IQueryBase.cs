using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZPW.Util.Domains.Repositories {

    /// <summary>
    /// 查询对象
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IQueryBase<TEntity>:IPager where TEntity:class,IAggregateRoot {

        /// <summary>
        /// 获取谓词
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> GetPredicate();

        /// <summary>
        /// 获取排序
        /// </summary>
        /// <returns></returns>
        string GetOrderBy();
    }
}
