using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ZPW.Util.Extensions {
    /// <summary>
    /// 表达式扩展
    /// </summary>
    public static partial class Extensions {
        #region Value(获取Lambda表达式的值)

        /// <summary>
        /// 获取Lambda表达式的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public static object Value<T>(this Expression<Func<T,bool>> expression) {
            return Lambda.GetValue(expression);
        }
        #endregion
    }
}
