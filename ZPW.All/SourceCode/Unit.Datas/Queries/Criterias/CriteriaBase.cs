using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZPW.Util.Domains;
using ZPW.Util.Domains.Repositories;

namespace Unit.Datas.Queries.Criterias {
    /// <summary>
    /// 查询条件基类
    /// </summary>
    public abstract class CriteriaBase<TEntity>:ICriteria<TEntity> where TEntity:class,IAggregateRoot {

        /// <summary>
        /// 谓词
        /// </summary>
        protected Expression<Func<TEntity,bool>> Predicate { get; set; }

        /// <summary>
        /// 获取谓词
        /// </summary>
        /// <returns></returns>
        public virtual Expression<Func<TEntity,bool>> GetPredicate() {
            return Predicate;
        }
    }
}
