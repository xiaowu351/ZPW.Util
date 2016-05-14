using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Datas.EF.Tests.Samples;
using Util.Datas.EF;
using ZPW.Util.Domains;

namespace Unit.Datas.EF.Tests.Repositories {
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeRepository : Repository<EmployeeTest> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        } 
    }
}
