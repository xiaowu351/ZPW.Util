using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unit.Datas.Extensions;
using Unit.Datas.Queries.Criterias;
using Unit.Datas.Queries.OrderBys;
using ZPW.Util.Core;
using ZPW.Util.Domains;
using ZPW.Util.Domains.Repositories;
using ZPW.Util.Extensions;

namespace Unit.Datas.Queries {
    public class Query<TEntity, TKey> : Pager, IQuery<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TKey> {

        /// <summary>
        /// 查询条件
        /// </summary>
        private ICriteria<TEntity> Criteria { get; set; }
        private OrderBuilder OrderBuilder {get;set;}

        public Query() {
            //OrderByBuilder = new OrderByBuilder();
        }

        public Query(IPager pager) : this() {
            Page = pager.Page;
            PageSize = pager.PageSize;
            TotalCount = pager.TotalCount;
            //OrderBy(pager.Order);
        }

        /// <summary>
        /// 获取谓词
        /// </summary>
        /// <returns></returns>
        public Expression<Func<TEntity, bool>> GetPredicate() {
            if (Criteria == null)
                return null;
            return Criteria.GetPredicate();
        }

        #region GetOrderBy(获取排序)

        /// <summary>
        /// 获取排序
        /// </summary>
        public string GetOrderBy() {
            Order = OrderBuilder.Generate();
            if (string.IsNullOrWhiteSpace(Order))
                Order = "Id desc";
            return Order;
        }

        #endregion

        #region 过滤条件
        /// <summary>
        /// 添加谓词,仅能添加一个条件,如果参数值为空，则忽略该条件
        /// </summary>
        /// <param name="predicate">谓词</param>
        /// <param name="isOr">是否使用Or连接</param>
        /// <returns></returns>
        public IQuery<TEntity, TKey> Filter(Expression<Func<TEntity, bool>> predicate, bool isOr = false) {
            predicate = QueryHelper.ValidatePredicate(predicate);
            if (predicate == null)
                return this;
            if (isOr) {
                Or(predicate);
            }
            else {
                And(predicate);
            }

            return this;
        }

        /// <summary>
        /// 过滤条件
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        /// <param name="operator">运算符</param>
        /// <returns></returns>
        public IQuery<TEntity, TKey> Filter(string propertyName, object value, Operator @operator = Operator.Equal) {
            return Filter(LambdaHelper.ParsePredicate<TEntity>(propertyName, value, @operator));
        }

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns></returns>
        public IQuery<TEntity, TKey> Filter(ICriteria<TEntity> criteria) {
            And(criteria.GetPredicate());
            return this;
        }

        /// <summary>
        /// 过滤Int数值段
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式，范例： t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public IQuery<TEntity, TKey> FilterInt<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max) {
            return Filter(new IntSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        }

        /// <summary>
        /// 过滤double数值段
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式，范例： t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public IQuery<TEntity, TKey> FilterDouble<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, double? min, double? max) {
            return Filter(new DoubleSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        }

        /// <summary>
        /// 过滤日期段，不包含时间
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Date</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public IQuery<TEntity, TKey> FilterDate<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max) {
            return Filter(new DateSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        }

        /// <summary>
        /// 过滤日期时间段，包含时间
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public IQuery<TEntity, TKey> FilterDateTime<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max) {
            return Filter(new DateTimeSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        }

        /// <summary>
        /// 过滤decimal数值段
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式，范例：t => t.Age</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public IQuery<TEntity, TKey> FilterDecimal<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, decimal? min, decimal? max) {
            return Filter(new DecimalSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        } 
        #endregion

        #region 连接

        public IQuery<TEntity, TKey> And(IQuery<TEntity, TKey> query) {
            return And(query.GetPredicate());
        }

        /// <summary>
        /// 与连接，将传入的查询条件合并到当前对象
        /// </summary>
        /// <param name="predicate">谓词</param>
        /// <returns></returns>
        public IQuery<TEntity, TKey> And(Expression<Func<TEntity, bool>> predicate) {
            if(Criteria == null) {
                Criteria = new Criteria<TEntity>(predicate);
                return this;
            }

            Criteria = new AndCriteria<TEntity>(Criteria.GetPredicate(), predicate);
            return this;
        }

        /// <summary>
        /// 或连接,将传入的查询条件合并到当前对象
        /// </summary>
        /// <param name="query">查询对象</param>
        public IQuery<TEntity, TKey> Or(IQuery<TEntity, TKey> query) {
            return Or(query.GetPredicate());
        }

        /// <summary>
        /// 或连接,将传入的查询条件合并到当前对象
        /// </summary>
        /// <param name="query">查询对象</param>
        public IQuery<TEntity, TKey> Or(Expression<Func<TEntity, bool>> predicate) {
            if (Criteria == null) {
                Criteria = new Criteria<TEntity>(predicate);
                return this;
            }

            Criteria = new OrCriteria<TEntity>(Criteria.GetPredicate(),predicate);
            return this;
        }
        #endregion
        #region OrderBy排序

        /// <summary>
        /// 添加排序，支持多次调用OrderBy创建多级排序
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="desc">是否降序</param>
        public IQuery<TEntity, TKey> OrderBy<TProperty>(Expression<Func<TEntity, TProperty>> expression, bool desc = false) {
            return OrderBy(LambdaHelper.GetName(expression), desc);
        }

        public IQuery<TEntity, TKey> OrderBy(string propertyName, bool desc = false) {
            OrderBuilder.Add(propertyName, desc);
            GetOrderBy();
            return this;
        }
        #endregion
        #region 清理
        /// <summary>
        /// 清理排序及查询条件
        /// </summary>
        public void Clear() {
            Criteria = null;
            OrderBuilder = new OrderBuilder();
        }
        #endregion

        #region GetList(获取列表)
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryable">数据源</param>
        /// <returns></returns>
        public List<TEntity> GetList(IQueryable<TEntity> queryable) {
            
            return Execute(queryable).OrderBy(Order).ToList();
        }

        /// <summary>
        /// 执行过滤和分页
        /// </summary>
        /// <param name="queryable">数据源</param>
        /// <returns></returns>
        private IQueryable<TEntity> Execute(IQueryable<TEntity> queryable) {
            queryable.CheckNull("queryable");
            queryable = FilterBy(queryable);
            GetOrderBy();
            return queryable;

        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="queryable">数据源</param>
        /// <returns></returns>
        private IQueryable<TEntity> FilterBy(IQueryable<TEntity> queryable) {
            if (Criteria == null)
                return queryable;
            return queryable.Where(Criteria.GetPredicate());
        }
        #endregion


        #region GetPagerList(获取分页列表)
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="queryable">数据源</param>
        /// <returns></returns>
        public PagerList<TEntity> GetPagerList(IQueryable<TEntity> queryable) {
            return Execute(queryable).PagerResult(this);
        }   
        #endregion


    }
}
