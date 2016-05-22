using Unit.Datas.EF.Tests.Samples;
using Util.Datas.EF;

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
