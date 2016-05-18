using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Extensions;
using System.Linq.Expressions;
using ZPW.Util.Test.Samples;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZPW.Util.Logging;
using ZPW.Util.Core;

namespace ZPW.Util.Test.Extensions {
    /// <summary>
    /// Lambda表达式操作测试
    /// </summary>
    [TestClass]
    public class LambdaTest {
        #region GetName(获取成员名称)

        /// <summary>
        /// 获取成员名称
        /// </summary>
        [TestMethod]
        public void TestGetName() {
            //空值返回空字符串
            Assert.AreEqual("", LambdaHelper.GetName(null));

            //返回一级属性名
            Expression<Func<LambdaTest1, string>> expression = test => test.Name;
            Assert.AreEqual("Name", LambdaHelper.GetName(expression));

            //返回二级属性名
            Expression<Func<LambdaTest1, string>> expression2 = test => test.A.Address;
            Assert.AreEqual("A.Address", LambdaHelper.GetName(expression2));

            //返回三级属性名
            Expression<Func<LambdaTest1, string>> expression3 = test => test.A.B.Name;
            Assert.AreEqual("A.B.Name", LambdaHelper.GetName(expression3));

            //测试可空整型
            Expression<Func<LambdaTest1, int?>> expression4 = test => test.NullableInt;
            Assert.AreEqual("NullableInt", LambdaHelper.GetName(expression4));

            //测试类型转换
            Expression<Func<LambdaTest1, int?>> expression5 = test => test.Age;
            Assert.AreEqual("Age", LambdaHelper.GetName(expression5));
        }

        #endregion

        #region GetValue(获取成员值)

        /// <summary>
        /// 获取成员值,委托返回类型为Object
        /// </summary>
        [TestMethod]
        public void TestGetValue_Object() {
            Expression<Func<LambdaModelTest, object>> expression = test => test.Name == "A";
            Assert.AreEqual("A", LambdaHelper.GetValue(expression));
        }

        /// <summary>
        /// 获取成员值,委托返回类型为bool
        /// </summary>
        [TestMethod]
        public void TestGetValue_Boolean() {
            //空值返回null
            Assert.AreEqual(null, LambdaHelper.GetValue(null));

            //一级返回值
            Expression<Func<LambdaModelTest, bool>> expression = test => test.Name == "A";
            Assert.AreEqual("A", LambdaHelper.GetValue(expression));

            //二级返回值
            Expression<Func<LambdaModelTest, bool>> expression2 = test => test.A.Integer == 1;
            Assert.AreEqual(1, LambdaHelper.GetValue(expression2));

            //三级返回值
            Expression<Func<LambdaModelTest, bool>> expression3 = test => test.A.B.Name == "B";
            Assert.AreEqual("B", LambdaHelper.GetValue(expression3));
        }

        /// <summary>
        /// 获取可空类型的值
        /// </summary>
        [TestMethod]
        public void TestGetValue_Nullable() {
            //可空整型
            Expression<Func<LambdaModelTest, bool>> expression = test => test.NullableInt == 1;
            Assert.AreEqual(1, LambdaHelper.GetValue(expression));

            //可空decimal
            expression = test => test.NullableDecimal == 1.5M;
            Assert.AreEqual(1.5M, LambdaHelper.GetValue(expression));
        }

        /// <summary>
        /// 获取成员值，运算符为方法
        /// </summary>
        [TestMethod]
        public void TestGetValue_Method() {
            //1级返回值
            Expression<Func<LambdaModelTest, bool>> expression = t => t.Name.Contains("A");
            Assert.AreEqual("A", LambdaHelper.GetValue(expression));

            //二级返回值
            expression = t => t.A.Address.Contains("B");
            Assert.AreEqual("B", LambdaHelper.GetValue(expression));

            //三级返回值
            expression = t => t.A.B.Name.StartsWith("C");
            Assert.AreEqual("C", LambdaHelper.GetValue(expression));
        }

        /// <summary>
        /// 从实例中获取值
        /// </summary>
        [TestMethod]
        public void TestGetValue_Instance() {
            var test = new LambdaTest1() { Name = "a", A = new LambdaTest1.TestA() { Address = "b", B = new LambdaTest1.TestA.TestB() { Name = "c" } } };

            //一级属性
            Expression<Func<string>> expression = () => test.Name;
            Assert.AreEqual("a", LambdaHelper.GetValue(expression));

            //二级属性
            Expression<Func<string>> expression2 = () => test.A.Address;
            Assert.AreEqual("b", LambdaHelper.GetValue(expression2));

            //三级属性
            Expression<Func<string>> expression3 = () => test.A.B.Name;
            Assert.AreEqual("c", LambdaHelper.GetValue(expression3));
        }

        /// <summary>
        /// 测试值为复杂类型
        /// </summary>
        [TestMethod]
        public void TestGetValue_Complex() {
            var test = new LambdaTest1() { Name = "a", A = new LambdaTest1.TestA() { Address = "b" } };

            //获取表达式的值
            Expression<Func<LambdaTest1, bool>> expression = t => t.Name == test.Name;
            Assert.AreEqual("a", LambdaHelper.GetValue(expression), "==test.Name");
            Expression<Func<LambdaTest1, bool>> expression2 = t => t.Name == test.A.Address;
            Assert.AreEqual("b", LambdaHelper.GetValue(expression2), "==test.A.Address");

            //获取方法的值
            Expression<Func<LambdaTest1, bool>> expression3 = t => t.Name.Contains(test.Name);
            Assert.AreEqual("a", LambdaHelper.GetValue(expression3), "Contains test.Name");
            Expression<Func<LambdaTest1, bool>> expression4 = t => t.Name.Contains(test.A.Address);
            Assert.AreEqual("b", LambdaHelper.GetValue(expression4), "==test.A.Address");
        }
        #endregion

        #region GetParameter(获取参数)

        /// <summary>
        /// 获取参数
        /// </summary>
        [TestMethod]
        public void TestGetParameter() {
            //空值返回null
            Assert.AreEqual(null, LambdaHelper.GetParameter(null));

            //一级返回值
            Expression<Func<LambdaTest1, object>> expression = test => test.Name == "A";
            Assert.AreEqual("test", LambdaHelper.GetParameter(expression).ToString());

            //二级返回值
            Expression<Func<LambdaTest1, object>> expression2 = test => test.A.Integer == 1;
            Assert.AreEqual("test", LambdaHelper.GetParameter(expression2).ToString());

            //三级返回值
            Expression<Func<LambdaTest1, object>> expression3 = test => test.A.B.Name == "B";
            Assert.AreEqual("test", LambdaHelper.GetParameter(expression3).ToString());
        }

        #endregion

        /// <summary>
        /// 测试值为枚举
        /// </summary>
        [TestMethod]
        public void TestGetValue_Enum() {
            var lambdaTestEnum = new LambdaTest1();

            lambdaTestEnum.NullableEnumValue = LogType.Error;

            //属性为枚举，值为枚举
            Expression<Func<LambdaTest1, bool>> expression = test => test.EnumValue == LogType.Debug;
            Assert.AreEqual(LogType.Debug.Value(), LambdaHelper.GetValue(expression));

            //属性为枚举,值为可空枚举
            expression = test => test.EnumValue == lambdaTestEnum.NullableEnumValue;
            Assert.AreEqual(LogType.Error, LambdaHelper.GetValue(expression));

            //属性为可空枚举,值为枚举
            expression = test => test.NullableEnumValue == LogType.Debug;
            Assert.AreEqual(LogType.Debug, LambdaHelper.GetValue(expression));

            //属性为可空枚举,值为可空枚举
            expression = test => test.NullableEnumValue == lambdaTestEnum.NullableEnumValue;
            Assert.AreEqual(LogType.Error, LambdaHelper.GetValue(expression));

            //属性为可空枚举,值为null
            lambdaTestEnum.NullableEnumValue = null;
            expression = test => test.NullableEnumValue == lambdaTestEnum.NullableEnumValue;
            Assert.AreEqual(null, LambdaHelper.GetValue(expression));

        }

        #region .Net Framework 4.6 支持
        /*
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

        #region Equal(创建等于表达式)

        /// <summary>
        /// 创建等于表达式
        /// </summary>
        [TestMethod]
        public void TestEqual() {
            //一级属性
            Expression<Func<LambdaTest1, bool>> expected = t => t.Age == 1;
            Assert.AreEqual(expected.ToString(), Lambda.Equal<LambdaTest1>("Age", 1).ToString());

            //二级属性
            Expression<Func<LambdaTest1, bool>> expected2 = t => t.A.Integer == 1;
            Assert.AreEqual(expected2.ToString(), Lambda.Equal<LambdaTest1>("A.Integer", 1).ToString());
        }

        #endregion

        #region NotEqual(创建不等于表达式)

        /// <summary>
        /// 创建不等于表达式
        /// </summary>
        [TestMethod]
        public void TestNotEqual() {
            //一级属性
            Expression<Func<LambdaTest1, bool>> expected = t => t.Age != 1;
            Assert.AreEqual(expected.ToString(), Lambda.NotEqual<LambdaTest1>("Age", 1).ToString());

            //二级属性
            Expression<Func<LambdaTest1, bool>> expected2 = t => t.A.Integer != 1;
            Assert.AreEqual(expected2.ToString(), Lambda.NotEqual<LambdaTest1>("A.Integer", 1).ToString());
        }

        #endregion

        #region Greater(创建大于表达式)

        /// <summary>
        /// 创建大于表达式
        /// </summary>
        [TestMethod]
        public void TestGreater() {
            //一级属性
            Expression<Func<LambdaTest1, bool>> expected = t => t.Age > 1;
            Assert.AreEqual(expected.ToString(), Lambda.Greater<LambdaTest1>("Age", 1).ToString());

            //二级属性
            Expression<Func<LambdaTest1, bool>> expected2 = t => t.A.Integer > 1;
            Assert.AreEqual(expected2.ToString(), Lambda.Greater<LambdaTest1>("A.Integer", 1).ToString());
        }

        #endregion

        #region Less(创建小于表达式)

        /// <summary>
        /// 创建小于表达式
        /// </summary>
        [TestMethod]
        public void TestLess() {
            //一级属性
            Expression<Func<LambdaTest1, bool>> expected = t => t.Age < 1;
            Assert.AreEqual(expected.ToString(), Lambda.Less<LambdaTest1>("Age", 1).ToString());

            //二级属性
            Expression<Func<LambdaTest1, bool>> expected2 = t => t.A.Integer < 1;
            Assert.AreEqual(expected2.ToString(), Lambda.Less<LambdaTest1>("A.Integer", 1).ToString());
        }

        #endregion

        #region GreaterEqual(创建大于等于表达式)

        /// <summary>
        /// 创建大于等于表达式
        /// </summary>
        [TestMethod]
        public void TestGreaterEqual() {
            //一级属性
            Expression<Func<LambdaTest1, bool>> expected = t => t.Age >= 1;
            Assert.AreEqual(expected.ToString(), Lambda.GreaterEqual<LambdaTest1>("Age", 1).ToString());

            //二级属性
            Expression<Func<LambdaTest1, bool>> expected2 = t => t.A.Integer >= 1;
            Assert.AreEqual(expected2.ToString(), Lambda.GreaterEqual<LambdaTest1>("A.Integer", 1).ToString());
        }

        #endregion

        #region LessEqual(创建小于等于表达式)

        /// <summary>
        /// 创建小于等于表达式
        /// </summary>
        [TestMethod]
        public void TestLessEqual() {
            //一级属性
            Expression<Func<LambdaTest1, bool>> expected = t => t.Age <= 1;
            Assert.AreEqual(expected.ToString(), Lambda.LessEqual<LambdaTest1>("Age", 1).ToString());

            //二级属性
            Expression<Func<LambdaTest1, bool>> expected2 = t => t.A.Integer <= 1;
            Assert.AreEqual(expected2.ToString(), Lambda.LessEqual<LambdaTest1>("A.Integer", 1).ToString());
        }

        #endregion

        #region Contains(调用Contains方法)

        /// <summary>
        /// 调用Contains方法
        /// </summary>
        [TestMethod]
        public void TestContains() {
            //一级属性
            Expression<Func<LambdaTest1, bool>> expected = t => t.Name.Contains("A");
            Assert.AreEqual(expected.ToString(), Lambda.Contains<LambdaTest1>("Name", "A").ToString());

            //二级属性
            Expression<Func<LambdaTest1, bool>> expected2 = t => t.A.Address.Contains("A");
            Assert.AreEqual(expected2.ToString(), Lambda.Contains<LambdaTest1>("A.Address", "A").ToString());

            //三级属性
            Expression<Func<LambdaTest1, bool>> expected3 = t => t.A.B.Name.Contains("A");
            Assert.AreEqual(expected3.ToString(), Lambda.Contains<LambdaTest1>("A.B.Name", "A").ToString());
        }

        #endregion

        #region Starts(调用StartsWith方法)

        /// <summary>
        /// 调用StartsWith方法
        /// </summary>
        [TestMethod]
        public void TestStarts() {
            //一级属性
            Expression<Func<LambdaTest1, bool>> expected = t => t.Name.StartsWith("A");
            Assert.AreEqual(expected.ToString(), Lambda.Starts<LambdaTest1>("Name", "A").ToString());

            //二级属性
            Expression<Func<LambdaTest1, bool>> expected2 = t => t.A.Address.StartsWith("A");
            Assert.AreEqual(expected2.ToString(), Lambda.Starts<LambdaTest1>("A.Address", "A").ToString());

            //三级属性
            Expression<Func<LambdaTest1, bool>> expected3 = t => t.A.B.Name.StartsWith("A");
            Assert.AreEqual(expected3.ToString(), Lambda.Starts<LambdaTest1>("A.B.Name", "A").ToString());
        }

        #endregion

        #region Ends(调用EndsWith方法)

        /// <summary>
        /// 调用EndsWith方法
        /// </summary>
        [TestMethod]
        public void TestEnds() {
            //一级属性
            Expression<Func<LambdaTest1, bool>> expected = t => t.Name.EndsWith("A");
            Assert.AreEqual(expected.ToString(), Lambda.Ends<LambdaTest1>("Name", "A").ToString());

            //二级属性
            Expression<Func<LambdaTest1, bool>> expected2 = t => t.A.Address.EndsWith("A");
            Assert.AreEqual(expected2.ToString(), Lambda.Ends<LambdaTest1>("A.Address", "A").ToString());

            //三级属性
            Expression<Func<LambdaTest1, bool>> expected3 = t => t.A.B.Name.EndsWith("A");
            Assert.AreEqual(expected3.ToString(), Lambda.Ends<LambdaTest1>("A.B.Name", "A").ToString());
        }

        #endregion

        #region GetConst(获取常量表达式)

        /// <summary>
        /// 获取常量表达式
        /// </summary>
        [TestMethod]
        public void TestGetConst() {
            Expression<Func<LambdaTest1, int?>> property = t => t.NullableInt;
            ConstantExpression constantExpression = Lambda.Constant(property, 1);
            Assert.AreEqual(typeof(int), constantExpression.Type);
        }

        #endregion

        #region GetAttribute(获取特性)

        /// <summary>
        /// 测试获取特性
        /// </summary>
        [TestMethod]
        public void TestGetAttribute() {
            DisplayAttribute attribute = Lambda.GetAttribute<LambdaTest1, string, DisplayAttribute>(t => t.Name);
            Assert.AreEqual("名称", attribute.Name);
        }

        #endregion

        #region GetAttributes(获取特性列表)

        /// <summary>
        /// 测试获取特性列表
        /// </summary>
        [TestMethod]
        public void TestGetAttributes() {
            IEnumerable<ValidationAttribute> attributes = Lambda.GetAttributes<LambdaTest1, string, ValidationAttribute>(t => t.Name);
            Assert.AreEqual(2, attributes.Count());
        }

        #endregion 
    */
        #endregion
    }
}
