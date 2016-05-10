using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZPW.Util.Validations
{
    /// <summary>
    /// 验证规则接口
    /// </summary>
    public interface  IValidationRule
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        ValidationResult Validate();
    }
}
