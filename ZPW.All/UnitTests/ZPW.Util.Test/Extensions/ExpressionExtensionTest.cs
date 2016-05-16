using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using ZPW.Util.Extensions;

namespace ZPW.Util.Test.Extensions {
    /// <summary>
    /// 表达式扩展测试
    /// </summary>
    [TestClass]
    public class ExpressionExtensionTest {
        #region 测试初始化

        /// <summary>
        /// 参数表达式
        /// </summary>
        private ParameterExpression _parameterExpression;

        /// <summary>
        /// 表达式1
        /// </summary>
        private Expression _expression1;

        /// <summary>
        /// 表达式2
        /// </summary>
        private Expression _expression2;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void TestInit() {
            _parameterExpression = Expression.Parameter(typeof(Person), "t");
            _expression1 = _parameterExpression.Property("Name").Call("Contains", Expression.Constant("A"));
            _expression2 = _parameterExpression.Property("Birthday")
                    .Property("Value")
                    .Property("Year")
                    .Greater(Expression.Constant(2000));
        }

        #endregion

        #region And(与操作)

        /// <summary>
        /// 测试And方法，连接两个表达式
        /// </summary>
        [TestMethod]
        public void TestAnd() {
            var andExpression = _expression1.And(_expression2).ToLambda<Func<Person, bool>>(_parameterExpression);
            Expression<Func<Person, bool>> expected = t => t.Name.Contains("A") && t.Birthday.Value.Year > 2000;
            Assert.AreEqual(expected.ToString(), andExpression.ToString());
        }

        /// <summary>
        /// 测试And方法，连接两个表达式
        /// </summary>
        [TestMethod]
        public void TestAnd_2() {
            Expression<Func<Person, bool>> left = t => t.Name == "A";
            Expression<Func<Person, bool>> right = t => t.Name == "B";
            Expression<Func<Person, bool>> expected = t => t.Name == "A" && t.Name == "B";
            Assert.AreEqual(expected.ToString(), left.And(right).ToString());
        }

        #endregion

        #region Or(或操作)

        /// <summary>
        /// 测试Or方法，连接两个表达式
        /// </summary>
        [TestMethod]
        public void TestOr() {
            var andExpression = _expression1.Or(_expression2).ToLambda<Func<Person, bool>>(_parameterExpression);
            Expression<Func<Person, bool>> expected = t => t.Name.Contains("A") || t.Birthday.Value.Year > 2000;
            Assert.AreEqual(expected.ToString(), andExpression.ToString());
        }

        /// <summary>
        /// 测试Or方法，连接两个表达式
        /// </summary>
        [TestMethod]
        public void TestOr_2() {
            Expression<Func<Person, bool>> left = t => t.Name == "A";
            Expression<Func<Person, bool>> right = t => t.Name == "B";
            Expression<Func<Person, bool>> expected = t => t.Name == "A" || t.Name == "B";
            Assert.AreEqual(expected.ToString(), left.Or(right).ToString());
        }

        #endregion

        #region Value(获取成员值)

        /// <summary>
        /// 获取成员值
        /// </summary>
        [TestMethod]
        public void TestValue() {
            Expression<Func<Person, bool>> expression = test => test.Name == "A";
            Assert.AreEqual("A", expression.Value());
        }

        #endregion

        #region 运算符操作

        /// <summary>
        /// 测试相等
        /// </summary>
        [TestMethod]
        public void TestEqual_Nullable() {
            _expression1 = _parameterExpression.Property("Age").Equal(1);
            Assert.AreEqual("t => (t.Age == 1)",
                _expression1.ToLambda<Func<Person, bool>>(_parameterExpression).ToString());
        }

        /// <summary>
        /// 测试不相等
        /// </summary>
        [TestMethod]
        public void TestNotEqual_Nullable() {
            _expression1 = _parameterExpression.Property("Age").NotEqual(1);
            Assert.AreEqual("t => (t.Age != 1)",
                _expression1.ToLambda<Func<Person, bool>>(_parameterExpression).ToString());
        }

        /// <summary>
        /// 测试大于
        /// </summary>
        [TestMethod]
        public void TestGreater_Nullable() {
            _expression1 = _parameterExpression.Property("Age").Greater(1);
            Assert.AreEqual("t => (t.Age > 1)",
                _expression1.ToLambda<Func<Person, bool>>(_parameterExpression).ToString());
        }

        /// <summary>
        /// 测试大于等于
        /// </summary>
        [TestMethod]
        public void TestGreaterEqual_Nullable() {
            _expression1 = _parameterExpression.Property("Age").GreaterEqual(1);
            Assert.AreEqual("t => (t.Age >= 1)",
                _expression1.ToLambda<Func<Person, bool>>(_parameterExpression).ToString());
        }

        /// <summary>
        /// 测试小于
        /// </summary>
        [TestMethod]
        public void TestLess_Nullable() {
            _expression1 = _parameterExpression.Property("Age").Less(1);
            Assert.AreEqual("t => (t.Age < 1)",
                _expression1.ToLambda<Func<Person, bool>>(_parameterExpression).ToString());
        }

        /// <summary>
        /// 测试小于等于
        /// </summary>
        [TestMethod]
        public void TestLessEqual_Nullable() {
            _expression1 = _parameterExpression.Property("Age").LessEqual(1);
            Assert.AreEqual("t => (t.Age <= 1)",
                _expression1.ToLambda<Func<Person, bool>>(_parameterExpression).ToString());
        }

        #endregion

        #region Person（测试类）

        /// <summary>
        /// 测试
        /// </summary>
        public class Person {
            public string Name { get; set; }
            public int? Age { get; set; }
            public DateTime? Birthday { get; set; }
        }

        #endregion
    }
}
