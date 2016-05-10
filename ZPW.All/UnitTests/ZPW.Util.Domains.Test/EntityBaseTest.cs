using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ZPW.Util.Domains.Tests.Samples;

namespace ZPW.Util.Domains.Tests
{
    [TestClass]
    public class EntityBaseTest
    {

        /// <summary>
        /// 测试实体1
        /// </summary>
        private Test _test1;
        /// <summary>
        /// 测试实体2
        /// </summary>
        private Test _test2;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            _test1 = new Test();
            _test2 = new Test();
        }

        /// <summary>
        /// 通过构造方法设置标识
        /// </summary>
        [TestMethod]
        public void TestId()
        {
            Guid id = Guid.NewGuid();
            _test1 = new Test(id);
            Assert.AreEqual(id, _test1.Id);
        }

        /// <summary>
        /// 新创建的实体不相等
        /// </summary>
        [TestMethod]
        public void TestNewEntityIsNotEquals()
        {
            Assert.IsFalse(_test1.Equals(_test2));
            Assert.IsFalse(_test1.Equals(null));

            Assert.IsFalse(_test1 == _test2);
            Assert.IsFalse(_test1 == null);
            Assert.IsFalse(null == _test2);

            Assert.IsTrue(_test1 != _test2);
            Assert.IsTrue(_test1 != null);
            Assert.IsTrue(null != _test2);
        }

        /// <summary>
        /// 当两个实体的标识相同，则实体相同
        /// </summary>
        [TestMethod]
        public void TestEntityEquals_IdEquals()
        {
            Guid id = Guid.NewGuid();
            _test1 = new Test(id);
            _test2 = new Test(id);
            Assert.IsTrue(_test1.Equals(_test2));
            Assert.IsTrue(_test1 == _test2);
            Assert.IsFalse(_test1 != _test2);
        }

        /// <summary>
        /// 测试状态输出
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            _test1 = new Test { Name = "a" };
            Assert.AreEqual(string.Format("Id:{0},姓名:a", _test1.Id), _test1.ToString());
        }
    }
}

