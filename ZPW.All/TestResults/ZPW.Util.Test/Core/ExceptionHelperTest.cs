using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Core;

namespace ZPW.Util.Test.Core
{
	/// <summary>
	/// ExceptionHelperTest 的摘要说明
	/// </summary>
	[TestClass]
	public class ExceptionHelperTest
	{
		public ExceptionHelperTest()
		{
			//
			//TODO: 在此处添加构造函数逻辑
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///获取或设置测试上下文，该上下文提供
		///有关当前测试运行及其功能的信息。
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region 附加测试特性
		//
		// 编写测试时，可以使用以下附加特性:
		//
		// 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// 在运行每个测试之前，使用 TestInitialize 来运行代码
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// 在每个测试运行完之后，使用 TestCleanup 来运行代码
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		#region CheckStringIsNullOrEmpty
		/// <summary>
		/// 测试CheckStringIsNullOrEmpty，如果字符串值为空串，应该抛出ArgumentException
		/// </summary>
		[TestMethod]
		[Description("测试CheckStringIsNullOrEmpty，如果字符串值为空串，应该抛出ArgumentException")]
		[ExpectedException(typeof(ArgumentException))]
		public void CheckStringIsNullOrEmptyWithEmptyStringParamTest()
		{
			CoreHelper.ExceptionHelper.CheckStringIsNullOrEmpty(string.Empty, "stringParam");
		}

		/// <summary>
		/// 测试CheckStringIsNullOrEmpty，如果字符串值为null，应该抛出ArgumentException
		/// </summary>
		[TestMethod]
		[Description("测试CheckStringIsNullOrEmpty，如果字符串值为null，应该抛出ArgumentException")]
		[ExpectedException(typeof(ArgumentException))]
		public void CheckStringIsNullOrEmptyWithNullStringParamTest()
		{
			CoreHelper.ExceptionHelper.CheckStringIsNullOrEmpty(null, "stringParam");
		}

		/// <summary>
		/// 测试CheckStringIsNullOrEmpty，如果是字符串，应该抛出ArgumentException
		/// </summary>
		[TestMethod]
		[Description("测试CheckStringIsNullOrEmpty，如果是字符串，则不抛出异常")]
		public void CheckStringIsNullOrEmptyWithNormalStringParamTest()
		{
			CoreHelper.ExceptionHelper.CheckStringIsNullOrEmpty("Normal String", "stringParam");
		}
		#endregion


		#region FalseThrowTest
		/// <summary>
		/// FalseThrow测试，参数为true时，不抛出异常
		/// </summary>
		[TestMethod]
		[Description("FalseThrow测试，参数为true时，不抛出异常")]
		public void FalseThrowWithTrueException()
		{
			CoreHelper.ExceptionHelper.FalseThrow(true, "FalseThrowTest");
		}

		/// <summary>
		/// FalseThrow测试，参数为false时，则抛出异常(系统默认异常)
		/// </summary>
		[TestMethod]
		[Description("FalseThrow测试，参数为false时，则抛出异常")]
		[ExpectedException(typeof(ApplicationException))]
		public void FalseThrowWithFalseException()
		{
			CoreHelper.ExceptionHelper.FalseThrow(false, "FalseThrowTest{0}","zhangsan");
		}

		/// <summary>
		/// FalseThrow测试，参数为false时，则抛出自定义的异常类
		/// </summary>
		[TestMethod]
		[Description("FalseThrow测试，参数为false时，则抛出自定义的异常类")]
		[ExpectedException(typeof(InvalidOperationException))]
		public void FalseThrowWithSpecifiedException()
		{
			CoreHelper.ExceptionHelper.FalseThrow<InvalidOperationException>(false, "FalseThrowTest");
		}
		#endregion


		#region TrueThrowTest
		/// <summary>
		/// TrueThrow测试，参数为true时，则抛出自定义的异常类
		/// </summary>
		[TestMethod]
		[Description("TrueThrow测试，参数为true时，则抛出自定义的异常类")]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TrueThrowWithSpecifiedException()
		{
			CoreHelper.ExceptionHelper.TrueThrow<InvalidOperationException>(true, "TrueThrowTest");
		}
		#endregion
	}
}
