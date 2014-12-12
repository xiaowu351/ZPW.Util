﻿using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32.SafeHandles;
using ZPW.Util.Core;

namespace ZPW.Util.Test.Core
{
	/// <summary>
	/// IOHelper帮助类测试
	/// </summary>
	[TestClass]
	public class IOHelperTest
	{
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

		private const string fileName = "out.txt";



		#region OccupyDirFileHandle
		/// <summary>
		/// 测试文件独占，文件夹和文件类似
		/// </summary>
		[TestMethod]
		public void OccupyDirFileHandleDirectoryTest()
		{
			//独占本地不存在文件夹
			using (SafeFileHandle fileHandle = CoreHelper.IOHelper.OccupyDirFileHandle(string.Format("{0}test", TestContext.TestDir)))
			{
				Assert.AreEqual(true, fileHandle.IsInvalid);
			}

			//尝试独占测试的临时目录
			using (SafeFileHandle fileHandle = CoreHelper.IOHelper.OccupyDirFileHandle(TestContext.TestDir))
			{
				Assert.AreEqual(false, fileHandle.IsInvalid);
			}
		}


		/// <summary>
		/// 测试文件独占时，参数异常
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void OccupyDirFileHandlerArgumentNullExceptionTest()
		{
			CoreHelper.IOHelper.OccupyDirFileHandle(string.Empty);
			CoreHelper.IOHelper.OccupyDirFileHandle(null);
		}
		#endregion

		#region CheckDirectoryIsUsed
		/// <summary>
		/// 检查存在的文件夹是否被占用
		/// </summary>
		[TestMethod]
		public void CheckDirectoryIsUsedTest()
		{
			//测试本地文件夹
			bool used = CoreHelper.IOHelper.CheckDirectoryIsUsed(new DirectoryInfo(TestContext.TestDir));
			Assert.AreEqual(true, used);
		}

		/// <summary>
		/// 检查不存在的文件夹是否被占用
		/// </summary>
		[TestMethod]
		public void CheckNoExistDirectoryIsUsed()
		{
			//独占本地不存在文件夹
			bool noUsed = CoreHelper.IOHelper.CheckDirectoryIsUsed(new DirectoryInfo(TestContext.TestDir + "test"));
			Assert.AreEqual(false, noUsed);
		}

		/// <summary>
		/// 检查文件夹占用下，返回占用文件名称
		/// </summary>
		[TestMethod]
		public void CheckDirectoryIsUsedOutFile()
		{
			string fileTarget = string.Empty;
			bool used = CoreHelper.IOHelper.CheckDirectoryIsUsed(new DirectoryInfo(TestContext.TestDir), out fileTarget);
			Assert.AreEqual(true, used);
			Trace.WriteLine(fileTarget);
		}
		#endregion
	}
}
