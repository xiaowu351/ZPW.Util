using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Core;

namespace ZPW.Util.Test.Core
{
	[TestClass]
	public class UriHelperTest
	{
		/// <summary>
		/// 获取uri参数集合
		/// </summary>
		[TestMethod]
		public void GetUriParamCollectionTest()
		{
			NameValueCollection collections =
				CoreHelper.UriHelper.GetUriParamCollection(new Uri("http://localhost/zhoupingwu?a=1&b=2"));
			foreach (var item in collections.AllKeys)
			{
				Trace.WriteLine(string.Format("{0}:\t{1}",item,collections[item]));
			}
		}
		/// <summary>
		/// 获取url上标签的值
		/// </summary>
		[TestMethod]
		public void GetBookmarkStringInUrlTest()
		{
			string bookmarkString = CoreHelper.UriHelper.GetBookmarkStringInUrl("http://localhost/zhoupingwu?test=1#littleTurtle");
			Assert.AreEqual("#littleTurtle", bookmarkString);
		}
	}
}
