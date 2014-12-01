using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Extensions;

namespace ZPW.Util.Test.Extensions
{
	/// <summary>
	/// 类型转换扩展测试
	/// </summary>
	[TestClass]
	public class ConvertExtensionTest
	{
			/// <summary>
			/// 转换为整数
			/// </summary>
			[TestMethod]
			public void TestToInt()
			{
				string obj1 = "";
				string obj2 = "1";
				Assert.AreEqual(0, obj1.ToInt());
				Assert.AreEqual(1, obj2.ToInt());
			}

			/// <summary>
			/// 转换为可空整数
			/// </summary>
			[TestMethod]
			public void TestToIntOrNull()
			{
				string obj1 = "";
				string obj2 = "1";
				Assert.IsNull(obj1.ToIntOrNull());
				Assert.AreEqual(1, obj2.ToIntOrNull());
			}

			/// <summary>
			/// 转换为双精度浮点数
			/// </summary>
			[TestMethod]
			public void TestToDouble()
			{
				string obj1 = "";
				string obj2 = "1.2";
				Assert.AreEqual(0, obj1.ToDouble());
				Assert.AreEqual(1.2, obj2.ToDouble());
			}

			/// <summary>
			/// 转换为可空双精度浮点数
			/// </summary>
			[TestMethod]
			public void TestToDoubleOrNull()
			{
				string obj1 = "";
				string obj2 = "1.2";
				Assert.IsNull(obj1.ToDoubleOrNull());
				Assert.AreEqual(1.2, obj2.ToDoubleOrNull());
			}

			/// <summary>
			/// 转换为高精度浮点数
			/// </summary>
			[TestMethod]
			public void TestToDecimal()
			{
				string obj1 = "";
				string obj2 = "1.2";
				Assert.AreEqual(0, obj1.ToDecimal());
				Assert.AreEqual(1.2M, obj2.ToDecimal());
			}

			/// <summary>
			/// 转换为可空高精度浮点数
			/// </summary>
			[TestMethod]
			public void TestToDecimalOrNull()
			{
				string obj1 = "";
				string obj2 = "1.2";
				Assert.IsNull(obj1.ToDecimalOrNull());
				Assert.AreEqual(1.2M, obj2.ToDecimalOrNull());
			}

			/// <summary>
			/// 转换为日期
			/// </summary>
			[TestMethod]
			public void TestToDate()
			{
				string obj1 = "";
				string obj2 = "2000-1-1";
				Assert.AreEqual(DateTime.MinValue, obj1.ToDate());
				Assert.AreEqual(new DateTime(2000, 1, 1), obj2.ToDate());
			}

			/// <summary>
			/// 转换为可空日期
			/// </summary>
			[TestMethod]
			public void TestToDateOrNull()
			{
				string obj1 = "";
				string obj2 = "2000-1-1";
				Assert.IsNull(obj1.ToDateOrNull());
				Assert.AreEqual(new DateTime(2000, 1, 1), obj2.ToDateOrNull());
			}

			/// <summary>
			/// 转换为Guid
			/// </summary>
			[TestMethod]
			public void TestToGuid()
			{
				string obj1 = "";
				string obj2 = "B9EB56E9-B720-40B4-9425-00483D311DDC";
				Assert.AreEqual(Guid.Empty, obj1.ToGuid());
				Assert.AreEqual(new Guid(obj2), obj2.ToGuid());
			}

			/// <summary>
			/// 转换为可空Guid
			/// </summary>
			[TestMethod]
			public void TestToGuidOrNull()
			{
				string obj1 = "";
				string obj2 = "B9EB56E9-B720-40B4-9425-00483D311DDC";
				Assert.IsNull(obj1.ToGuidOrNull());
				Assert.AreEqual(new Guid(obj2), obj2.ToGuidOrNull());
			}

			/// <summary>
			/// 转换为Guid集合,值为字符串
			/// </summary>
			[TestMethod]
			public void TestToGuidList_String()
			{
				const string guid = "83B0233C-A24F-49FD-8083-1337209EBC9A,,EAB523C6-2FE7-47BE-89D5-C6D440C3033A,";
				Assert.AreEqual(2, guid.ToGuidList().Count);
				Assert.AreEqual(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"), guid.ToGuidList()[0]);
				Assert.AreEqual(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"), guid.ToGuidList()[1]);
			}

			/// <summary>
			/// 转换为Guid集合,值为字符串集合
			/// </summary>
			[TestMethod]
			public void TestToGuidList_StringList()
			{
				var list = new List<string> { "83B0233C-A24F-49FD-8083-1337209EBC9A", "EAB523C6-2FE7-47BE-89D5-C6D440C3033A" };
				Assert.AreEqual(2, list.ToGuidList().Count);
				Assert.AreEqual(new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"), list.ToGuidList()[0]);
				Assert.AreEqual(new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A"), list.ToGuidList()[1]);
			}

			/// <summary>
			/// 转换为字符串
			/// </summary>
			[TestMethod]
			public void TestToStr()
			{
				object value = null;
				Assert.AreEqual(string.Empty, value.ToStr());
				value = 1;
				Assert.AreEqual("1", value.ToStr());
			}
		}
	
}
