using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Core;

namespace ZPW.Util.Test.Core
{
	[TestClass]
	public class UuidHelperTest
	{
		/// <summary>
		/// 测试调用Windows API UuidCreateSequential生成连续GUID
		/// </summary>
		[TestMethod]
		public void NewGuidTest()
		{
			for (int i = 0; i < 50; i++)
			{
				Trace.WriteLine(CoreHelper.UuidHelper.NewGuid().ToString());
			}
		}

		/// <summary>
		/// 测试Windows API UuidCreateSequential生成的连续GUID字符串
		/// </summary>
		[TestMethod]
		public void NewUuidStringTest()
		{
			for (int i = 0; i < 50; i++)
			{
				Trace.WriteLine(CoreHelper.UuidHelper.NewUuidString());
			}
		}
	}
}
