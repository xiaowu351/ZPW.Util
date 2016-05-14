using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Domains.Repositories;
using ZPW.Util.Domains.Tests.Samples;

namespace ZPW.Util.Domains.Tests.Repositories {
    /// <summary>
    /// 分页集合测试
    /// </summary>
    [TestClass]
    public class PagerListTest {
        /// <summary>
        /// 分页集合
        /// </summary>
        private PagerList<Employee> _list;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void TestInit() {
            _list = new PagerList<Employee>(1, 2, 3);
            _list.Add(new Employee());
            _list.Add(new Employee() { Name = "B" });
        }

        /// <summary>
        /// 元素个数
        /// </summary>
        [TestMethod]
        public void TestCount() {
            Assert.AreEqual(2, _list.Count);
        }

        /// <summary>
        /// 用索引获取元素
        /// </summary>
        [TestMethod]
        public void TestIndex() {
            Assert.AreEqual("B", _list[1].Name);
        }

        /// <summary>
        /// 转换类型
        /// </summary>
        [TestMethod]
        public void TestConvert() {
            var result = _list.Convert(t => new EmployeeDto());
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result.Page);
            Assert.AreEqual(2, result.PageSize);
            Assert.AreEqual(3, result.TotalCount);
            Assert.AreEqual(2, result.PageCount);
        }
    }
}
