using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using ZPW.Util.Exceptions;
using ZPW.Util.Logging;

namespace ZPW.Util.Test.Exceptions {
    /// <summary>
    /// WarningTest 的摘要说明
    /// 应用程序异常测试
    /// </summary>
    [TestClass]
	public class WarningTest
	{
		public WarningTest()
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

		#region 常量

		/// <summary>
		/// 异常消息
		/// </summary>
		public const string Message = "A";

		/// <summary>
		/// 异常消息2
		/// </summary>
		public const string Message2 = "B";

		/// <summary>
		/// 异常消息3
		/// </summary>
		public const string Message3 = "C";

		/// <summary>
		/// 异常消息4
		/// </summary>
		public const string Message4 = "D";

		#endregion

		#region TestValidate_MessageIsNull(验证消息为空)

		/// <summary>
		/// 验证消息为空
		/// </summary>
		[TestMethod]
		public void TestValidate_MessageIsNull()
		{
			Warning Warning = new Warning(null, "P1");
			Assert.AreEqual(string.Empty, Warning.Message);
		}

		#endregion

		#region TestCode(设置错误码)

		/// <summary>
		/// 设置错误码
		/// </summary>
		[TestMethod]
		public void TestCode()
		{
			Warning Warning = new Warning(Message, "P1");
			Assert.AreEqual("P1", Warning.Code);
		}

		#endregion

		#region TestLogLevel(测试日志级别)

		/// <summary>
		/// 测试日志级别
		/// </summary>
		[TestMethod]
		public void TestLogLevel()
		{
			Warning Warning = new Warning(Message, "P1", EnumLogLevel.Fatal);
			Assert.AreEqual(EnumLogLevel.Fatal, Warning.Level);
		}

		#endregion

		#region TestMessage_OnlyMessage(仅设置消息)

		/// <summary>
		/// 仅设置消息
		/// </summary>
		[TestMethod]
		public void TestMessage_OnlyMessage()
		{
			Warning Warning = new Warning(Message);
			Assert.AreEqual(Message, Warning.Message);
		}

		#endregion

		#region TestMessage_OnlyException(仅设置异常)

		/// <summary>
		/// 仅设置异常
		/// </summary>
		[TestMethod]
		public void TestMessage_OnlyException()
		{
			Warning Warning = new Warning(GetException());
			Assert.AreEqual(Message, Warning.Message);
		}

		/// <summary>
		/// 获取异常
		/// </summary>
		private Exception GetException()
		{
			return new Exception(Message);
		}

		#endregion

		#region TestMessage_Message_Exception(设置错误消息和异常)

		/// <summary>
		/// 设置错误消息和异常
		/// </summary>
		[TestMethod]
		public void TestMessage_Message_Exception()
		{
			Warning Warning = new Warning(Message2, "P1", EnumLogLevel.Fatal, GetException());
			Assert.AreEqual(string.Format("{0}\r\n{1}", Message2, Message), Warning.Message);
		}

		#endregion

		#region TestMessage_2LayerException(设置2层异常)

		/// <summary>
		/// 设置2层异常
		/// </summary>
		[TestMethod]
		public void TestMessage_2LayerException()
		{
			Warning Warning = new Warning(Message3, "P1", EnumLogLevel.Fatal, Get2LayerException());
			Assert.AreEqual(string.Format("{0}\r\n{1}\r\n{2}", Message3, Message2, Message), Warning.Message);
		}

		/// <summary>
		/// 获取2层异常
		/// </summary>
		private Exception Get2LayerException()
		{
			return new Exception(Message2, new Exception(Message));
		}

		#endregion

		#region TestMessage_Warning(设置Warning异常)

		/// <summary>
		/// 设置Warning异常
		/// </summary>
		[TestMethod]
		public void TestMessage_Warning()
		{
			Warning Warning = new Warning(GetWarning());
			Assert.AreEqual(Message, Warning.Message);
		}

		/// <summary>
		/// 获取异常
		/// </summary>
		private Warning GetWarning()
		{
			return new Warning(Message);
		}

		#endregion

		#region TestMessage_2LayerWarning(设置2层Warning异常)

		/// <summary>
		/// 设置2层Warning异常
		/// </summary>
		[TestMethod]
		public void TestMessage_2LayerWarning()
		{
			Warning Warning = new Warning(Message3, "", Get2LayerWarning());
			Assert.AreEqual(string.Format("{0}\r\n{1}\r\n{2}", Message3, Message2, Message), Warning.Message);
		}

		/// <summary>
		/// 获取2层异常
		/// </summary>
		private Warning Get2LayerWarning()
		{
			return new Warning(Message2, "", new Warning(Message));
		}

		#endregion

		#region TestMessage_3LayerWarning(设置3层Warning异常)

		/// <summary>
		/// 设置3层Warning异常
		/// </summary>
		[TestMethod]
		public void TestMessage_3LayerWarning()
		{
			Warning Warning = new Warning(Message4, "", Get3LayerWarning());
			Assert.AreEqual(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}", Message4, Message3, Message2, Message), Warning.Message);
		}

		/// <summary>
		/// 获取3层异常
		/// </summary>
		private Warning Get3LayerWarning()
		{
			return new Warning(Message3, "", new Exception(Message2, new Warning(Message)));
		}

		#endregion

		#region 添加异常数据

		/// <summary>
		/// 添加异常数据
		/// </summary>
		[TestMethod]
		public void TestAdd_1Layer()
		{
			Warning Warning = new Warning(Message);
			Warning.Data.Add("key1", "value1");
			Warning.Data.Add("key2", "value2");

			StringBuilder expected = new StringBuilder();
			expected.AppendLine(Message);
			expected.AppendLine("key1:value1");
			expected.AppendLine("key2:value2");
			Assert.AreEqual(expected.ToString(), Warning.Message);
		}

		/// <summary>
		/// 添加异常数据
		/// </summary>
		[TestMethod]
		public void TestAdd_2Layer()
		{
			Exception exception = new Exception(Message);
			exception.Data.Add("a", "a1");
			exception.Data.Add("b", "b1");

			Warning Warning = new Warning(exception);
			Warning.Data.Add("key1", "value1");
			Warning.Data.Add("key2", "value2");

			StringBuilder expected = new StringBuilder();
			expected.AppendLine(Message);
			expected.AppendLine("a:a1");
			expected.AppendLine("b:b1");
			expected.AppendLine("key1:value1");
			expected.AppendLine("key2:value2");
			Assert.AreEqual(expected.ToString(), Warning.Message);
		}

		#endregion
	}
}
