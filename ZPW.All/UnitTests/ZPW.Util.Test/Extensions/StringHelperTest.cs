using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZPW.Util.Core;
using ZPW.Util.Extensions;

namespace ZPW.Util.Test.Extensions
{
	/// <summary>
	///字符串扩展类
	/// </summary>
	[TestClass]
	public class StringHelperTest
	{

		#region 全角半角转换
		/// <summary>
		/// 全角转半角
		/// </summary>
		[TestMethod]
		public void ToDBCTest()
		{
			string source = "１２３１２３";
			string target = "123123";
			string result = source.ToDBC();
			Assert.AreEqual(target,result);
		}
		/// <summary>
		/// 半角转全角
		/// </summary>
		[TestMethod]
		public void ToSBCTest()
		{
			string source = "123";
			string target = "１２３";
			string result = source.ToSBC();
			Assert.AreEqual(target,result);
		}
		#endregion

		#region 身份证校验
		/// <summary>
		///  身份证校验
		/// </summary>
		[TestMethod]
		[Description("身份证校验")]
		public void CheckIdCardTest()
		{
			//18位身份证校验
			Assert.AreEqual(("34052419800101001X").CheckIdCard(), EnumIdCardResult.Success);
			Assert.AreEqual(StringHelper.CheckIdCard("340524198001010013"), EnumIdCardResult.ErrorCard);
			Assert.AreEqual(StringHelper.CheckIdCard("340524198013010013"), EnumIdCardResult.ErrorBirthday);
			Assert.AreEqual(StringHelper.CheckIdCard("010524198001010013"), EnumIdCardResult.ErrorProvince);
			Assert.AreEqual(StringHelper.CheckIdCard("010524198001010a13"), EnumIdCardResult.ErrorString);

			//15位身份证校验
			Assert.AreEqual(StringHelper.CheckIdCard("360521790717485"), EnumIdCardResult.Success);
			Assert.AreEqual(StringHelper.CheckIdCard("360521791717485"), EnumIdCardResult.ErrorBirthday);
			Assert.AreEqual(StringHelper.CheckIdCard("010521790717485"), EnumIdCardResult.ErrorProvince);
			Assert.AreEqual(StringHelper.CheckIdCard("36052179071748a"), EnumIdCardResult.ErrorString);
			Assert.AreEqual(StringHelper.CheckIdCard("36052179071748123123"), EnumIdCardResult.ErrorString);

		}

		/// <summary>
		/// 测试Convert15to18
		/// </summary>
		[TestMethod]
		[Description("测试Convert15to18")]
		public void Convert15to18Test()
		{
			string source = "360521790717485";
			string target = "360521197907174854";

			string result = source.ConvertIdCard15To18();
			Assert.AreEqual(target, result);
		} 
		#endregion

		#region 常用正则表达式实现

		[TestMethod]
		[Description("测试IsValidateEmail")]
		public void IsValidateEmailTest()
		{
			Assert.IsFalse(StringHelper.IsValidateEmail("11"));
			Assert.IsFalse(StringHelper.IsValidateEmail("1@1"));
			Assert.IsTrue(StringHelper.IsValidateEmail("haiguan.china@ccic.intra.customs.gov.cn"));
			Assert.IsTrue(StringHelper.IsValidateEmail("151@yahoo.com.cn"));
			Assert.IsTrue(StringHelper.IsValidateEmail("151@163.com"));
		}

		[TestMethod]
		[Description("测试IsValidateInteger")]
		public void IsValidateIntegerTest()
		{
			Assert.IsFalse(StringHelper.IsValidateInteger("11.1"));
			Assert.IsFalse(StringHelper.IsValidateInteger("1aa"));
			Assert.IsFalse(StringHelper.IsValidateInteger("0.99"));
			Assert.IsTrue(StringHelper.IsValidateInteger("123"));
			Assert.IsFalse(StringHelper.IsValidateInteger("09"));
			Assert.IsFalse(StringHelper.IsValidateInteger("-1123"));
		}

		[TestMethod]
		[Description("测试IsValidateIpAddress")]
		public void IsValidateIpAddressTest()
		{
			Assert.IsFalse(StringHelper.IsValidateIpAddress("11.1"));
			Assert.IsFalse(StringHelper.IsValidateIpAddress("11.90.280.128"));
			Assert.IsTrue(StringHelper.IsValidateIpAddress("89.89.0.01"));
			Assert.IsTrue(StringHelper.IsValidateIpAddress("10.99.35.130"));
			Assert.IsTrue(StringHelper.IsValidateIpAddress("127.0.0.1"));
			Assert.IsTrue(StringHelper.IsValidateIpAddress("192.168.0.1"));
		}

		[TestMethod]
		[Description("测试IsValidateMoney")]
		public void IsValidateMoneyTest()
		{
			Assert.IsFalse(StringHelper.IsValidateMoney("11.12312"));
			Assert.IsFalse(StringHelper.IsValidateMoney("-1.89"));
			Assert.IsFalse(StringHelper.IsValidateMoney("9.0000"));
			Assert.IsFalse(StringHelper.IsValidateMoney("0.6"));
			Assert.IsTrue(StringHelper.IsValidateMoney("0.68"));
			Assert.IsTrue(StringHelper.IsValidateMoney("8.88"));
		}


		[TestMethod]
		[Description("测试IsValidateAlphaAndNumber")]
		public void IsValidateAlphaAndNumberTest()
		{
			Assert.IsFalse(StringHelper.IsValidateAlphaAndNumber("!af&asldfj"));
			Assert.IsTrue(StringHelper.IsValidateAlphaAndNumber("123asdfAsdf123"));
			Assert.IsFalse(StringHelper.IsValidateAlphaAndNumber("123_2asd"));
			Assert.IsTrue(StringHelper.IsValidateAlphaAndNumber("123jkasdfasd123"));
			Assert.IsTrue(StringHelper.IsValidateAlphaAndNumber("asdfasdf"));
			Assert.IsTrue(StringHelper.IsValidateAlphaAndNumber("88123"));
		}


		[TestMethod]
		[Description("测试IsValidateAlphaUpper")]
		public void IsValidateAlphaUpperTest()
		{
			Assert.IsFalse(StringHelper.IsValidateAlphaUpper("asdfAASDF"));
			Assert.IsTrue(StringHelper.IsValidateAlphaUpper("ASDFKLJ"));
			Assert.IsFalse(StringHelper.IsValidateAlphaUpper("ASDFJ_ASD"));
			Assert.IsFalse(StringHelper.IsValidateAlphaUpper(" ASDF"));
			Assert.IsFalse(StringHelper.IsValidateAlphaUpper("12ASDF"));
			Assert.IsFalse(StringHelper.IsValidateAlphaUpper("ASDF,ASDF"));
		}

		[TestMethod]
		[Description("测试IsValidateAlphaLower")]
		public void IsValidateAlphaLowerTest()
		{
			Assert.IsFalse(StringHelper.IsValidateAlphaLower("asdfAASDF"));
			Assert.IsTrue(StringHelper.IsValidateAlphaLower("asdfasdf"));
			Assert.IsFalse(StringHelper.IsValidateAlphaLower("asdf123"));
			Assert.IsFalse(StringHelper.IsValidateAlphaLower("as df"));
			Assert.IsFalse(StringHelper.IsValidateAlphaLower("asdf_asdf"));
			Assert.IsFalse(StringHelper.IsValidateAlphaLower("sA1"));
		}

		[TestMethod]
		[Description("测试IsValidateNubmer")]
		public void IsValidateNubmerTest()
		{
			Assert.IsFalse(StringHelper.IsValidateNubmer("1-"));
			Assert.IsTrue(StringHelper.IsValidateNubmer("123"));
			Assert.IsFalse(StringHelper.IsValidateNubmer("23;"));
			Assert.IsFalse(StringHelper.IsValidateNubmer("1.098"));
			Assert.IsFalse(StringHelper.IsValidateNubmer("0123"));
			
		}

		[TestMethod]
		[Description("测试IsValidateChinese")]
		public void IsValidateChineseTest()
		{
			Assert.IsFalse(StringHelper.IsValidateChinese("中a国"));
			Assert.IsTrue(StringHelper.IsValidateChinese("中国"));
			Assert.IsFalse(StringHelper.IsValidateChinese("中 国"));
			Assert.IsFalse(StringHelper.IsValidateChinese("中_国"));
			Assert.IsFalse(StringHelper.IsValidateChinese("中国 "));

		}

		[TestMethod]
		[Description("测试IsValidateUrl")]
		public void IsValidateUrlTest()
		{
			Assert.IsFalse(StringHelper.IsValidateUrl("http//ww.126.com"));
			Assert.IsTrue(StringHelper.IsValidateUrl("ftp://www.126.com"));
			Assert.IsFalse(StringHelper.IsValidateUrl("ftp//www.126.com"));
			Assert.IsTrue(StringHelper.IsValidateUrl("https://www.sina.com.cn"));
			Assert.IsTrue(StringHelper.IsValidateUrl("http://www.baidu.com"));
			Assert.IsTrue(StringHelper.IsValidateUrl("ftp://10.200.8.8"));

		}

		[TestMethod]
		[Description("测试IsValidateMobilePhone")]
		public void IsValidateMobilePhoneTest()
		{
			Assert.IsFalse(StringHelper.IsValidateMobilePhone("12345678901"));
			Assert.IsTrue(StringHelper.IsValidateMobilePhone("13512345678"));
			Assert.IsTrue(StringHelper.IsValidateMobilePhone("17012345678"));
			Assert.IsFalse(StringHelper.IsValidateMobilePhone("17198765432"));
			Assert.IsTrue(StringHelper.IsValidateMobilePhone("18987654321"));
			Assert.IsTrue(StringHelper.IsValidateMobilePhone("18587654321"));
			Assert.IsFalse(StringHelper.IsValidateMobilePhone("1901231238812"));
		}

		[TestMethod]
		[Description("测试IsValidateHomePhone")]
		public void IsValidateHomePhoneTest()
		{
			Assert.IsFalse(StringHelper.IsValidateHomePhone("010-123234234"));
			Assert.IsTrue(StringHelper.IsValidateHomePhone("010-67891234"));
			Assert.IsTrue(StringHelper.IsValidateHomePhone("01067891234"));

			Assert.IsFalse(StringHelper.IsValidateHomePhone("0792678912341"));
			Assert.IsTrue(StringHelper.IsValidateHomePhone("079267891234"));
			Assert.IsTrue(StringHelper.IsValidateHomePhone("0792-67891234"));
			Assert.IsTrue(StringHelper.IsValidateHomePhone("0106789123"));
			Assert.IsFalse(StringHelper.IsValidateHomePhone("010a123123"));
			Assert.IsFalse(StringHelper.IsValidateHomePhone("010a1231231"));
		}
		#endregion
	}
}
