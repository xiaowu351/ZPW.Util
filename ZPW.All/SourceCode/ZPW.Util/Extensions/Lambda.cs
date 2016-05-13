using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ZPW.Util.Extensions {
    /// <summary>
    /// Lambda表达式操作
    /// </summary>
    public class Lambda {

        #region GetValue（获取值）
        /// <summary>
        /// 获取值，范例：t => t.Name == "A"，返回A
        /// </summary>
        /// <param name="expression">表达式，范例：t => t.Name == "A"，返回A</param>
        /// <returns></returns>
        public static object GetValue(LambdaExpression expression) {
            if (expression == null)
                return null;

            BinaryExpression binaryExpression = GetBinaryExpression(expression);

            if (binaryExpression != null)
                return GetBinaryValue(binaryExpression);

            var callExpression = expression.Body as MethodCallExpression;
            if (callExpression != null)
                return GetMethodValue(callExpression);
            return null;
        }

        /// <summary>
        /// 获取方法调用表达式的值
        /// </summary>
        /// <param name="callExpression"></param>
        /// <returns></returns>
        private static object GetMethodValue(MethodCallExpression callExpression) {
            var argumentExpression = callExpression.Arguments.FirstOrDefault();
            return GetConstantValue(argumentExpression);
        }

        /// <summary>
        /// 获取二元表达式的值
        /// </summary>
        /// <param name="binaryExpression"></param>
        /// <returns></returns>
        private static object GetBinaryValue(BinaryExpression binaryExpression) {
            var unaryExpression = binaryExpression.Right as UnaryExpression;
            if (unaryExpression != null)
                return GetConstantValue(unaryExpression.Operand);
            return GetConstantValue(binaryExpression.Right);
        }

        /// <summary>
        /// 获取常量的值
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        private static object GetConstantValue(Expression expression) {
            var constantExpression = expression as ConstantExpression;
            if (constantExpression == null)
                return null;
            return constantExpression.Value;
        }

        /// <summary>
        /// 获取二元表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        private static BinaryExpression GetBinaryExpression(LambdaExpression expression) {
            var binaryExpression = expression.Body as BinaryExpression;
            if (binaryExpression != null)
                return binaryExpression;
            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression == null)
                return null;
            return unaryExpression.Operand as BinaryExpression;
        }
        #endregion

        #region GetCriteriaCount（获取谓词条件的个数）
        /// <summary>
        /// 获取谓词条件的个数
        /// </summary>
        /// <param name="expression">谓词表达式，范例： t => t.Name == "A"</param>
        /// <returns></returns>
        public static int GetCriteriaCount(LambdaExpression expression) {
            if (expression == null)
                return 0;
            var result = expression.ToString().Replace("AndAlso", "|").Replace("OrElse", "|");
            return result.Split('|').Count();
        }
        #endregion
    }
}
