using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZPW.Util.Extensions;

namespace ZPW.Util.Test.Extensions {
   
    /// <summary>
    /// 验证扩展测试
    /// </summary>
    [TestClass]
    public class ValidateExtensionTest {

        /// <summary>
        /// 检查空值，不为空则正常执行
        /// </summary>
        [TestMethod]
        public void TestCheckNull() {
            var test = new object();
            test.CheckNull("test");
        }

        /// <summary>
        /// 检查空值，值为null则抛出异常
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCheckNull_Null_Throw() {
            try {
                object test = null;
                test.CheckNull("test");
            }
            catch (ArgumentNullException ex) {
                Assert.IsTrue(ex.Message.Contains("test"), ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 测试是否空值
        /// </summary>
        [TestMethod]
        public void TestIsEmpty_String() {
            string value = null;
            Assert.IsTrue(value.IsEmpty());
            Assert.IsTrue("".IsEmpty());
            Assert.IsTrue(" ".IsEmpty());
            Assert.IsFalse("a".IsEmpty());
        }

        /// <summary>
        /// 测试是否空值
        /// </summary>
        [TestMethod]
        public void TestIsEmpty_Guid() {
            Guid value = Guid.Empty;
            Assert.IsTrue(value.IsEmpty());
            value = Guid.NewGuid();
            Assert.IsFalse(value.IsEmpty());
        }

        /// <summary>
        /// 测试是否空值
        /// </summary>
        [TestMethod]
        public void TestIsEmpty_Guid_Nullable() {
            Guid? value = null;
            Assert.IsTrue(value.IsEmpty());
            value = Guid.Empty;
            Assert.IsTrue(value.IsEmpty());
            value = Guid.NewGuid();
            Assert.IsFalse(value.IsEmpty());
        }
    }
}
