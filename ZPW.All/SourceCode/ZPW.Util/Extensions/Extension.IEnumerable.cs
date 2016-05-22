using System.Collections.Generic;
using ZPW.Util.Core;

namespace ZPW.Util.Extensions {

    /// <summary>
    /// IEnumerable接口扩展
    /// </summary>
    public static partial class Extension { 

        /// <summary>
        /// 转换为用分隔符拼接的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>        
        /// <param name="separator">分隔符，默认使用逗号分隔</param> 
        public static string Splice<T>(this IEnumerable<T> list, string separator = ",") {
            return CoreHelper.StringHelper.Splice(list, separator);
        }
    }
}
