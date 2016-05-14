using System;
using ZPW.Util.Domains.Base;

namespace ZPW.Util.Domains.Tests.Samples {
    /// <summary>
    /// 员工
    /// </summary>
    public class EmployeeDto : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public EmployeeDto() : this(Guid.Empty) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public EmployeeDto(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } 
        public override void Validate()
        {
            base.Validate();             
        }
    }
}
