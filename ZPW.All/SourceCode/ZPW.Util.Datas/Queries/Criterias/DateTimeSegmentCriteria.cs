using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZPW.Util.Domains;

namespace Unit.Datas.Queries.Criterias {
    /// <summary>
    /// 日期时间段过滤条件 -- 包含时间
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
   public  class DateTimeSegmentCriteria<TEntity,TProperty>:SegmentCriteria<TEntity,TProperty,DateTime> 
        where TEntity:class,IAggregateRoot {

        /// <summary>
        /// 初始化日期时间过滤条件
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public DateTimeSegmentCriteria(Expression<Func<TEntity,TProperty>> propertyExpression, DateTime? min, DateTime? max)
            :base(propertyExpression,min,max){

        }

        /// <summary>
        /// 最小值是否大于最大值
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        protected override bool IsMinGreaterMax(DateTime? min, DateTime? max) {
            return min > max;
        }
    }
}
