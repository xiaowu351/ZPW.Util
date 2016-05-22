using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Datas.EF.Tests.Repositories;
using ZPW.Util.Domains;
using ZPW.Util.Domains.Base;

namespace Unit.Datas.EF.Tests.Samples {
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeTest :  AggregateRoot {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public EmployeeTest(Guid id) : base(id) {
        }

        internal static EmployeeTest GetEmployee() {
            return new EmployeeTest(Guid.NewGuid()) { Name="1"};
        }

        internal static EmployeeTest GetEmployee1() {
            return new EmployeeTest(Guid.NewGuid()) { Name = "2" };
        }



        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }
 
    }
}
