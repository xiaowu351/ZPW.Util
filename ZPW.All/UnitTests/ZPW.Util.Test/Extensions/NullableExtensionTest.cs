using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Extensions;

namespace ZPW.Util.Test.Extensions {
    /// <summary>
    /// 可空类型扩展
    /// </summary>
    [TestClass]
    public class NullableExtensionTest {
        /// <summary>
        /// 测试可空整型
        /// </summary>
        [TestMethod]
        public void TestSafeValue_Int() {
            int? value = null;
            Assert.AreEqual(0, value.SafeValue());

            value = 1;
            Assert.AreEqual(1, value.SafeValue());
        }

        /// <summary>
        /// 测试可空Guid
        /// </summary>
        [TestMethod]
        public void TestSafeValue_Guid() {
            Guid? value = null;
            Assert.AreEqual(Guid.Empty, value.SafeValue());

            value = Guid.NewGuid();
            Assert.AreEqual(value.Value, value.SafeValue());
        }

        /// <summary>
        /// 测试可空DateTime
        /// </summary>
        [TestMethod]
        public void TestSafeValue_DateTime() {
            DateTime? value = null;
            Assert.AreEqual(DateTime.MinValue, value.SafeValue());

            value = "2000-1-1".ToDate();
            Assert.AreEqual(value.Value, value.SafeValue());
        }

        /// <summary>
        /// 测试可空bool
        /// </summary>
        [TestMethod]
        public void TestSafeValue_Boolean() {
            bool? value = null;
            Assert.AreEqual(false, value.SafeValue());

            value = true;
            Assert.AreEqual(true, value.SafeValue());
        }

        /// <summary>
        /// 测试可空double
        /// </summary>
        [TestMethod]
        public void TestSafeValue_Double() {
            double? value = null;
            Assert.AreEqual(0, value.SafeValue());

            value = 1.1;
            Assert.AreEqual(1.1, value.SafeValue());
        }

        /// <summary>
        /// 测试可空decimal
        /// </summary>
        [TestMethod]
        public void TestSafeValue_Decimal() {
            decimal? value = null;
            Assert.AreEqual(0, value.SafeValue());

            value = 1.1M;
            Assert.AreEqual(1.1M, value.SafeValue());
        }
    }
}
