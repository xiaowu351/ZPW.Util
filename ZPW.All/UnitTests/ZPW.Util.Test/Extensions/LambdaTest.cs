using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Extensions;
using System.Linq.Expressions;
using ZPW.Util.Test.Samples;

namespace ZPW.Util.Test.Extensions {
    /// <summary>
    /// Lambda表达式操作测试
    /// </summary>
    [TestClass]
    public class LambdaTest {
        #region GetValue(获取成员值)

        /// <summary>
        /// 获取成员值,委托返回类型为Object
        /// </summary>
        [TestMethod]
        public void TestGetValue_Object() {
            Expression<Func<LambdaModelTest, object>> expression = test => test.Name == "A";
            Assert.AreEqual("A", Lambda.GetValue(expression));
        }

        /// <summary>
        /// 获取成员值,委托返回类型为bool
        /// </summary>
        [TestMethod]
        public void TestGetValue_Boolean() {
            //空值返回null
            Assert.AreEqual(null, Lambda.GetValue(null));

            //一级返回值
            Expression<Func<LambdaModelTest, bool>> expression = test => test.Name == "A";
            Assert.AreEqual("A", Lambda.GetValue(expression));

            //二级返回值
            Expression<Func<LambdaModelTest, bool>> expression2 = test => test.A.Integer == 1;
            Assert.AreEqual(1, Lambda.GetValue(expression2));

            //三级返回值
            Expression<Func<LambdaModelTest, bool>> expression3 = test => test.A.B.Name == "B";
            Assert.AreEqual("B", Lambda.GetValue(expression3));
        }

        /// <summary>
        /// 获取可空类型的值
        /// </summary>
        [TestMethod]
        public void TestGetValue_Nullable() {
            //可空整型
            Expression<Func<LambdaModelTest, bool>> expression = test => test.NullableInt == 1;
            Assert.AreEqual(1, Lambda.GetValue(expression));

            //可空decimal
            expression = test => test.NullableDecimal == 1.5M;
            Assert.AreEqual(1.5M, Lambda.GetValue(expression));
        }

        /// <summary>
        /// 获取成员值，运算符为方法
        /// </summary>
        [TestMethod]
        public void TestGetValue_Method() {
            //1级返回值
            Expression<Func<LambdaModelTest, bool>> expression = t => t.Name.Contains("A");
            Assert.AreEqual("A", Lambda.GetValue(expression));

            //二级返回值
            expression = t => t.A.Address.Contains("B");
            Assert.AreEqual("B", Lambda.GetValue(expression));

            //三级返回值
            expression = t => t.A.B.Name.StartsWith("C");
            Assert.AreEqual("C", Lambda.GetValue(expression));
        }

        #endregion

        #region GetCriteriaCount(获取谓词条件的个数)

        /// <summary>
        /// 获取谓词条件的个数
        /// </summary>
        [TestMethod]
        public void TestGetCriteriaCount() {
            //0个条件
            Assert.AreEqual(0, Lambda.GetCriteriaCount(null));

            //1个条件
            Expression<Func<LambdaModelTest, bool>> expression = test => test.Name == "A";
            Assert.AreEqual(1, Lambda.GetCriteriaCount(expression));

            //2个条件，与连接符
            expression = test => test.Name == "A" && test.Name == "B";
            Assert.AreEqual(2, Lambda.GetCriteriaCount(expression));

            //2个条件，或连接符
            expression = test => test.Name == "A" || test.Name == "B";
            Assert.AreEqual(2, Lambda.GetCriteriaCount(expression));

            //3个条件
            expression = test => test.Name == "A" && test.Name == "B" || test.Name == "C";
            Assert.AreEqual(3, Lambda.GetCriteriaCount(expression));

            //3个条件,包括导航属性
            expression = test => test.A.Address == "A" && test.Name == "B" || test.Name == "C";
            Assert.AreEqual(3, Lambda.GetCriteriaCount(expression));
        }

        /// <summary>
        /// 获取谓词条件的个数，运算符为方法
        /// </summary>
        [TestMethod]
        public void TestGetCriteriaCount_Method() {
            //1个条件
            Expression<Func<LambdaModelTest, bool>> expression = t => t.Name.Contains("A");
            Assert.AreEqual(1, Lambda.GetCriteriaCount(expression));

            //2个条件,与连接
            expression = t => t.Name.Contains("A") && t.Name == "A";
            Assert.AreEqual(2, Lambda.GetCriteriaCount(expression));

            //2个条件,或连接,包含导航属性
            expression = t => t.Name.Contains("A") || t.A.Address == "A";
            Assert.AreEqual(2, Lambda.GetCriteriaCount(expression));
        }

        #endregion
    }
}
