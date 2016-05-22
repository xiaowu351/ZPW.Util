using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZPW.Util.Core;
using ZPW.Util.Extensions;

namespace Unit.Datas.Queries {
    /// <summary>
    /// 查询操作帮助类
    /// </summary>
    internal class QueryHelper {


        /// <summary>
        /// 验证谓词，无效时返回null
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">谓词</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> ValidatePredicate<T>(Expression<Func<T, bool>> predicate) {
            predicate.CheckNull("predicate");
            if (LambdaHelper.GetCriteriaCount(predicate) > 1) {
                throw new InvalidOperationException(string.Format("仅允许添加一个条件，条件：{0}", predicate));
            }
            if (predicate.Value() == null)
                return null;

            if (string.IsNullOrWhiteSpace(predicate.Value().ToString()))
                return null;

            return predicate;
        }
    }
}
