using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Lambdas;
using ZPW.Util.Test.Samples;
using System.Linq.Expressions;

namespace ZPW.Util.Test.Lambdas {
    /// <summary>
    /// 测试表达式生成器
    /// </summary>
    [TestClass]
    public class ExpressionBuilderTest {

        ExpressionBuilder<LambdaTest2> _builder;
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void TestInit() {
            _builder = new ExpressionBuilder<LambdaTest2>();
        }

        /// <summary>
        /// 测试创建表达式
        /// </summary>
        [TestMethod]
        public void TestCreate_Int() {
            Expression<Func<LambdaTest2, int>> property = t => t.Int;
            var expression = _builder.Create(property, Util.Core.Operator.Equal, 1);
            Expression<Func<LambdaTest2, bool>> expected = t => t.Int == 1;
            Assert.AreEqual(expected.ToString(), _builder.ToLambda(expression).ToString());
        }

        /// <summary>
        /// 测试创建表达式
        /// </summary>
        [TestMethod]
        public void TestCreate_Int_Nullable() {
            Expression<Func<LambdaTest2, int?>> property = t => t.NullableInt;
            var expression = _builder.Create(property, Util.Core.Operator.Equal, 1);
            Assert.AreEqual("t => (t.NullableInt == 1)",_builder.ToLambda(expression).ToString());
        }
    }
}
