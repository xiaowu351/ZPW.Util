﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZPW.Util.Core;
using ZPW.Util.Domains;
using ZPW.Util.Extensions;

namespace Unit.Datas.Queries.Criterias {
    /// <summary>
    /// 整数段过滤条件
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class IntSegmentCriteria<TEntity, TProperty> : SegmentCriteria<TEntity, TProperty, int>
         where TEntity : class, IAggregateRoot {

        /// <summary>
        /// 初始化整数段过滤条件
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public IntSegmentCriteria(Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max)
            : base(propertyExpression, min, max) {

        }

        /// <summary>
        /// 最小值是否大于最大值
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        protected override bool IsMinGreaterMax(int? min, int? max) {
            return min > max;
        } 
    }
}
