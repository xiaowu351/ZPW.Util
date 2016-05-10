using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPW.Util.Validations;
using ZPW.Util.Validations.Impl;

namespace ZPW.Util.Domains.Factory
{
    public  class ValidationFactory
    {
        /// <summary>
        /// 创建验证操作
        /// </summary>
        public static IValidation Create()
        {
            return new Validation();
        }
    }
}
