using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unit.Datas.Queries;
using Unit.Datas.Queries.Criterias;
using ZPW.Util.Domains;
using ZPW.Util.Domains.Repositories;
using ZPW.Util.Extensions;

namespace Unit.Datas.Extensions {
    /// <summary>
    /// 查询扩展
    /// </summary>
    public static class Extensions {
        /// <summary>
        /// 过滤整数段
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="propertyExpression">属性表达式，范例： t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static IQueryable<T> FilterInt<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> propertyExpression, int? min, int? max)
            where T : class, IAggregateRoot {
            return source.Filter(new IntSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }

        /// <summary>
        /// 过滤double数值段
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static IQueryable<T> FilterDouble<T, TProperty>(this IQueryable<T> source,
            Expression<Func<T, TProperty>> propertyExpression, double? min, double? max) where T : class, IAggregateRoot {
            return source.Filter(new DoubleSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }

        /// <summary>
        /// 过滤decimal数值段
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static IQueryable<T> FilterDecimal<T, TProperty>(this IQueryable<T> source,
            Expression<Func<T, TProperty>> propertyExpression, decimal? min, decimal? max) where T : class, IAggregateRoot {
            return source.Filter(new DecimalSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }

        /// <summary>
        /// 过滤日期段，不包含时间
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static IQueryable<T> FilterDate<T, TProperty>(this IQueryable<T> source,
            Expression<Func<T, TProperty>> propertyExpression, DateTime? min, DateTime? max) where T : class, IAggregateRoot {
            return source.Filter(new DateSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }

        /// <summary>
        /// 过滤日期时间段，包含时间
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static IQueryable<T> FilterDateTime<T, TProperty>(this IQueryable<T> source,
            Expression<Func<T, TProperty>> propertyExpression, DateTime? min, DateTime? max) where T : class, IAggregateRoot {
            return source.Filter(new DateTimeSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }
        /// <summary>
        /// Where条件过滤--扩展
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>
        public static IQueryable<T> Filter<T>(this IQueryable<T> source, ICriteria<T> criteria) where T : class, IAggregateRoot {
            if (criteria == null)
                return source;
            var predicate = criteria.GetPredicate();
            if (predicate == null)
                return source;
            return source.Where(predicate);
        }

        /// <summary>
        /// Where条件过滤扩展
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="queryable">查询对象</param>
        /// <param name="predicate">谓词</param>
        /// <returns></returns>
        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate) {

            QueryHelper.ValidatePredicate(predicate);
            if (predicate == null)
                return queryable;

            return queryable.Where(predicate);
        }

        /// <summary>
        /// 排序扩展（待实现）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="propertyName">排序属性名，，多个属性用逗号分隔，降序用desc字符串，范例：Name,Age desc</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName) {
            //待实现自动解析字符串排序-- 可以直接使用DynamicQueryable源码，这里通过dll引入
            return System.Linq.Dynamic.DynamicQueryable.OrderBy<T>(source, propertyName);
        }

        /// <summary>
        /// 创建分页列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="page">页索引，表示第几页，从1开始</param>
        /// <param name="pageSize">每页显示行数，默认20</param>
        public static PagerList<T> PagerResult<T>(this IQueryable<T> source, int page, int pageSize = 20) {
            return PagerResult(source, new Pager(page, pageSize));
        }

        /// <summary>
        /// 创建分页列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pager">分页对象</param>
        public static PagerList<T> PagerResult<T>(this IQueryable<T> source, IPager pager) {
            source = OrderBy(source, pager);
            source = Pager(source, pager);
            return CreatePageList(source, pager);
        }

        /// <summary>
        /// 排序
        /// </summary>
        private static IQueryable<T> OrderBy<T>(IQueryable<T> source, IPager pager) {
            if (pager.Order.IsEmpty())
                return source;
            return source.OrderBy(pager.Order);
        }

        /// <summary>
        /// 分页
        /// </summary>
        private static IQueryable<T> Pager<T>(IQueryable<T> source, IPager pager) {
            if (pager.TotalCount <= 0)
                pager.TotalCount = source.Count();
            return source.Skip(pager.SkipCount).Take(pager.PageSize);
        }

        /// <summary>
        /// 创建分页列表
        /// </summary>
        private static PagerList<T> CreatePageList<T>(IEnumerable<T> source, IPager pager) {
            var result = new PagerList<T>(pager);
            result.AddRange(source.ToList());
            return result;
        }
    }
}
