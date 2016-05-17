using System;
using System.Linq.Expressions;
using ZPW.Util.Core;
using ZPW.Util.Domains;
using ZPW.Util.Extensions;
using ZPW.Util.Lambdas;

namespace Unit.Datas.Queries.Criterias {
    /// <summary>
    /// 范围过滤条件基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public abstract class SegmentCriteria<TEntity, TProperty, TValue> : CriteriaBase<TEntity>
        where TEntity : class, IAggregateRoot
        where TValue : struct {

        /// <summary>
        /// 最小值
        /// </summary>
        public TValue? Min { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public TValue? Max { get; set; }

        /// <summary>
        /// 表达式生成器
        /// </summary>
        private ExpressionBuilder<TEntity> Builder { get; set; }
        /// <summary>
        /// 属性表达式
        /// </summary>
        public Expression<Func<TEntity, TProperty>> PropertyExpression { get; set; }

        /// <summary>
        /// 初始化段过滤条件
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        protected SegmentCriteria(Expression<Func<TEntity, TProperty>> propertyExpression, TValue? min, TValue? max) {
            Builder = new ExpressionBuilder<TEntity>();
            PropertyExpression = propertyExpression;
            Min = min;
            Max = max;
            if (IsMinGreaterMax(min, max)) {
                Min = max;
                Max = min;
            }
        }
        /// <summary>
        /// 最小值是否大于最大值
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        protected abstract bool IsMinGreaterMax(TValue? min, TValue? max);

        /// <summary>
        /// 获取谓词
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TEntity, bool>> GetPredicate() {
            var first = CreateLeftExpression();
            var second = CreateRightExpression();
            return Builder.ToLambda(first.And(second));
        }

        /// <summary>
        /// 创建左操作数，即： t => t.Property >= Min
        /// </summary>
        /// <returns></returns>
        private Expression CreateLeftExpression() {
            if (Min == null)
                return null;
            return Builder.Create(PropertyExpression, ZPW.Util.Core.Operator.GreaterEqual, GetMinValue());
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <returns></returns>
        protected virtual TValue? GetMinValue() {
            return Min;
        }

        /// <summary>
        /// 创建右操作数，即： t => t.Property &lt;= Max
        /// </summary>
        /// <returns></returns>
        private Expression CreateRightExpression() {
            if (Max == null)
                return null;
            return Builder.Create(PropertyExpression, GetMaxOperator(), GetMaxValue());
        }

        /// <summary>
        /// 获取最大值相关的运算符
        /// </summary>
        /// <returns></returns>
        protected virtual Operator GetMaxOperator() {
            return Operator.LessEqual;
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <returns></returns>
        protected virtual TValue? GetMaxValue() {
            return Max;
        }
    }
}
