using System.ComponentModel;

namespace Unit.Datas.Queries.OrderBys {
    /// <summary>
    /// 排序方向
    /// </summary>
    public enum OrderDirection {
        /// <summary>
        /// 升序
        /// </summary>
        [Description("升序")]
        Asc,
        /// <summary>
        /// 降序
        /// </summary>
        [Description("降序")]
        Desc
    }

    public static class OrderDirectionExtensions {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static string Description(this OrderDirection? direction) {
            return direction == null ? string.Empty : direction.Value.ToString();
        }
    }
}