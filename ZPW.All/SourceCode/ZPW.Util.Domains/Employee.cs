using System;
using System.ComponentModel.DataAnnotations;
using ZPW.Util.Domains.Base;

namespace ZPW.Util.Domains
{
    /// <summary>
    /// 员工
    /// </summary>
    public class Employee : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Employee() : this(Guid.Empty) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Employee(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别不能为空")]
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Range(18, 50, ErrorMessage = "年龄范围为18岁到50岁")]
        public int Age { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        [Required(ErrorMessage = "职业不能为空")]
        public string Job { get; set; }

        /// <summary>
        /// 工资
        /// </summary>
        public double Salary { get; set; }


        public override void Validate()
        {
            base.Validate();             
        }
    }
}
