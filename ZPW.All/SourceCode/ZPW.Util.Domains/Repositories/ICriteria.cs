using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZPW.Util.Domains.Repositories {
    /// <summary>
    /// 查询条件
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICriteria<TEntity> where TEntity:class,IAggregateRoot{

        /// <summary>
        /// 获取谓词
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> GetPredicate();
    }
}
