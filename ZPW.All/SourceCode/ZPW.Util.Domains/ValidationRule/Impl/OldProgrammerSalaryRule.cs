using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPW.Util.Validations;

namespace ZPW.Util.Domains.ValidationRule.Impl
{
    public class OldProgrammerSalaryRule : IValidationRule
    {
        private readonly Employee _employee;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="employee"></param>
        public OldProgrammerSalaryRule(Employee employee)
        {
            _employee = employee;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ValidationResult Validate()
        {
            if (_employee.Job == "程序员" && _employee.Age > 40 && _employee.Gender == "男" && _employee.Salary < 10000)
                return new ValidationResult("程序员老男人的工资不能低于1万");
            return ValidationResult.Success;
        }
    }
}
