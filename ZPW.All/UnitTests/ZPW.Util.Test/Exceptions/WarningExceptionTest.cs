using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Core;
using ZPW.Util.Logging;

namespace ZPW.Util.Test.Exceptions
{
	/// <summary>
	/// WarningExceptionTest 的摘要说明
	/// 应用程序异常测试
	/// </summary>
	[TestClass]
	public class WarningExceptionTest
	{
		public WarningExceptionTest()
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
			WarningException warningException = new WarningException(null, "P1");
			Assert.AreEqual(string.Empty, warningException.Message);
		}

		#endregion

		#region TestCode(设置错误码)

		/// <summary>
		/// 设置错误码
		/// </summary>
		[TestMethod]
		public void TestCode()
		{
			WarningException warningException = new WarningException(Message, "P1");
			Assert.AreEqual("P1", warningException.Code);
		}

		#endregion

		#region TestLogLevel(测试日志级别)

		/// <summary>
		/// 测试日志级别
		/// </summary>
		[TestMethod]
		public void TestLogLevel()
		{
			WarningException warningException = new WarningException(Message, "P1", EnumLogLevel.Fatal);
			Assert.AreEqual(EnumLogLevel.Fatal, warningException.Level);
		}

		#endregion

		#region TestMessage_OnlyMessage(仅设置消息)

		/// <summary>
		/// 仅设置消息
		/// </summary>
		[TestMethod]
		public void TestMessage_OnlyMessage()
		{
			WarningException warningException = new WarningException(Message);
			Assert.AreEqual(Message, warningException.Message);
		}

		#endregion

		#region TestMessage_OnlyException(仅设置异常)

		/// <summary>
		/// 仅设置异常
		/// </summary>
		[TestMethod]
		public void TestMessage_OnlyException()
		{
			WarningException warningException = new WarningException(GetException());
			Assert.AreEqual(Message, warningException.Message);
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
			WarningException warningException = new WarningException(Message2, "P1", EnumLogLevel.Fatal, GetException());
			Assert.AreEqual(string.Format("{0}\r\n{1}", Message2, Message), warningException.Message);
		}

		#endregion

		#region TestMessage_2LayerException(设置2层异常)

		/// <summary>
		/// 设置2层异常
		/// </summary>
		[TestMethod]
		public void TestMessage_2LayerException()
		{
			WarningException warningException = new WarningException(Message3, "P1", EnumLogLevel.Fatal, Get2LayerException());
			Assert.AreEqual(string.Format("{0}\r\n{1}\r\n{2}", Message3, Message2, Message), warningException.Message);
		}

		/// <summary>
		/// 获取2层异常
		/// </summary>
		private Exception Get2LayerException()
		{
			return new Exception(Message2, new Exception(Message));
		}

		#endregion

		#region TestMessage_Warning(设置WarningException异常)

		/// <summary>
		/// 设置WarningException异常
		/// </summary>
		[TestMethod]
		public void TestMessage_Warning()
		{
			WarningException warningException = new WarningException(GetWarning());
			Assert.AreEqual(Message, warningException.Message);
		}

		/// <summary>
		/// 获取异常
		/// </summary>
		private WarningException GetWarning()
		{
			return new WarningException(Message);
		}

		#endregion

		#region TestMessage_2LayerWarning(设置2层WarningException异常)

		/// <summary>
		/// 设置2层WarningException异常
		/// </summary>
		[TestMethod]
		public void TestMessage_2LayerWarning()
		{
			WarningException warningException = new WarningException(Message3, "", Get2LayerWarning());
			Assert.AreEqual(string.Format("{0}\r\n{1}\r\n{2}", Message3, Message2, Message), warningException.Message);
		}

		/// <summary>
		/// 获取2层异常
		/// </summary>
		private WarningException Get2LayerWarning()
		{
			return new WarningException(Message2, "", new WarningException(Message));
		}

		#endregion

		#region TestMessage_3LayerWarning(设置3层WarningException异常)

		/// <summary>
		/// 设置3层WarningException异常
		/// </summary>
		[TestMethod]
		public void TestMessage_3LayerWarning()
		{
			WarningException warningException = new WarningException(Message4, "", Get3LayerWarning());
			Assert.AreEqual(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}", Message4, Message3, Message2, Message), warningException.Message);
		}

		/// <summary>
		/// 获取3层异常
		/// </summary>
		private WarningException Get3LayerWarning()
		{
			return new WarningException(Message3, "", new Exception(Message2, new WarningException(Message)));
		}

		#endregion

		#region 添加异常数据

		/// <summary>
		/// 添加异常数据
		/// </summary>
		[TestMethod]
		public void TestAdd_1Layer()
		{
			WarningException warningException = new WarningException(Message);
			warningException.Data.Add("key1", "value1");
			warningException.Data.Add("key2", "value2");

			StringBuilder expected = new StringBuilder();
			expected.AppendLine(Message);
			expected.AppendLine("key1:value1");
			expected.AppendLine("key2:value2");
			Assert.AreEqual(expected.ToString(), warningException.Message);
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

			WarningException warningException = new WarningException(exception);
			warningException.Data.Add("key1", "value1");
			warningException.Data.Add("key2", "value2");

			StringBuilder expected = new StringBuilder();
			expected.AppendLine(Message);
			expected.AppendLine("a:a1");
			expected.AppendLine("b:b1");
			expected.AppendLine("key1:value1");
			expected.AppendLine("key2:value2");
			Assert.AreEqual(expected.ToString(), warningException.Message);
		}

		#endregion
	}
}
