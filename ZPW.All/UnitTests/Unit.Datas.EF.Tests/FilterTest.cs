using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit.Datas.EF.Tests.Repositories;
using Unit.Datas.EF.Tests.Samples;
using System.Linq;
using Unit.Datas.Extensions;

namespace Unit.Datas.EF.Tests {
    /// <summary>
    /// 过滤测试
    /// </summary>
    [TestClass]
    public class FilterTest {
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void TestInit() {
            EmployeeRepository repository = GetEmployeeRepository();
            repository.FindAll().Clear();
            repository.Add(EmployeeTest.GetEmployee());
            repository.Add(EmployeeTest.GetEmployee1());
        }

        /// <summary>
        /// 获取员工仓储
        /// </summary>
        private EmployeeRepository GetEmployeeRepository() {
            return new EmployeeRepository(new TestUnitOfWork());
        }

        /// <summary>
        /// 测试Filter过滤
        /// </summary>
        [TestMethod]
        public void TestFilter() {
            EmployeeRepository repository = GetEmployeeRepository();

            //用where查询
            var result = repository.Find().Where(t => t.Name == "");
            Assert.AreEqual(0, result.Count());

            //用Fileter查询
            result = repository.Find().Filter(t => t.Name == "");
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(EmployeeTest.GetEmployee().Name, result.ToList()[0].Name);
            Assert.AreEqual(EmployeeTest.GetEmployee1().Name, result.ToList()[1].Name);
        }
    }
}
