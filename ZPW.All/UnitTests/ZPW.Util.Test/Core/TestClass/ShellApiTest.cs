using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShellLib;

namespace ZPW.Util.Test.Core.TestClass
{
	[TestClass]
	public class ShellApiTest
	{
		/// <summary>
		/// 测试删除文件
		/// </summary>
		[TestMethod]
		public void ShellFileOperationTest()
		{

			ShellLib.ShellFileOperation fo = new ShellLib.ShellFileOperation();

			String[] source = new String[3];
			String[] dest = new String[3];

			source[0] = @"C:\TEMP\test.txt";
			source[1] = @"C:\TEMP\test1.txt";
			source[2] = @"C:\TEMP1";
			

			fo.Operation = ShellLib.ShellFileOperation.FileOperations.FO_DELETE;
			//fo.OwnerWindow = this.Handle;
			fo.SourceFiles = source;
			fo.DestFiles = dest;

			bool RetVal = fo.DoOperation();
			if (RetVal)
				Trace.WriteLine("Copy Complete without errors!");
			else
				Trace.WriteLine("Copy Complete with errors!");
			
		}
	}
}
