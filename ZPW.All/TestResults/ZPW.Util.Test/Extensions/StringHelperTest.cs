using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Core;
using ZPW.Util.Extensions;

namespace ZPW.Util.Test.Extensions
{
	/// <summary>
	/// 身份证校验帮助类
	/// </summary>
	[TestClass]
	public class StringHelperTest
	{
		[TestMethod]
		public void CheckIdCardTest()
		{
			Assert.AreEqual(StringHelper.CheckIdCard("34052419800101001X"), IdCardResult.Success);
			Assert.AreEqual(StringHelper.CheckIdCard("340524198001010013"), IdCardResult.ErrorCard);
		}
	}
}
