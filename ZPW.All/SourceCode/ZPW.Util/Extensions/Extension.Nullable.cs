﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZPW.Util.Extensions {
    /// <summary>
    /// 扩展-- 可空类型
    /// </summary>
    public static partial class Extension {

        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct {
            return value ?? default(T);
        }
    }
}
