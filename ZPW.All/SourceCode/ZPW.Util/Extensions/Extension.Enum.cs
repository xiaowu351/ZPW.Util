using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZPW.Util.Core;

namespace ZPW.Util.Extensions {
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static partial class Extension {

        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string Description(this System.Enum instance) {
            return CoreHelper.EnumHelper.GetDescription(instance.GetType(), instance);
        }
        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static int Value(this System.Enum instance) {
            return CoreHelper.EnumHelper.GetValue(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="instance">枚举实例</param>
        public static T Value<T>(this System.Enum instance) {
            return CoreHelper.ConvertHelper.To<T>(Value(instance));
        }

    }
}
