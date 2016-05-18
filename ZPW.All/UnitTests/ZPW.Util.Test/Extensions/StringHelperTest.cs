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
            Assert.AreEqual(Extenstion.CheckIdCard("340524198001010013"), EnumIdCardResult.ErrorCard);
            Assert.AreEqual(Extenstion.CheckIdCard("340524198013010013"), EnumIdCardResult.ErrorBirthday);
            Assert.AreEqual(Extenstion.CheckIdCard("010524198001010013"), EnumIdCardResult.ErrorProvince);
            Assert.AreEqual(Extenstion.CheckIdCard("010524198001010a13"), EnumIdCardResult.ErrorString);

            //15位身份证校验
            Assert.AreEqual(Extenstion.CheckIdCard("360521790717485"), EnumIdCardResult.Success);
            Assert.AreEqual(Extenstion.CheckIdCard("360521791717485"), EnumIdCardResult.ErrorBirthday);
            Assert.AreEqual(Extenstion.CheckIdCard("010521790717485"), EnumIdCardResult.ErrorProvince);
            Assert.AreEqual(Extenstion.CheckIdCard("36052179071748a"), EnumIdCardResult.ErrorString);
            Assert.AreEqual(Extenstion.CheckIdCard("36052179071748123123"), EnumIdCardResult.ErrorString);

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
            Assert.IsFalse(Extenstion.IsValidateEmail("11"));
            Assert.IsFalse(Extenstion.IsValidateEmail("1@1"));
            Assert.IsTrue(Extenstion.IsValidateEmail("haiguan.china@ccic.intra.customs.gov.cn"));
            Assert.IsTrue(Extenstion.IsValidateEmail("151@yahoo.com.cn"));
            Assert.IsTrue(Extenstion.IsValidateEmail("151@163.com"));
		}

		[TestMethod]
		[Description("测试IsValidateInteger")]
		public void IsValidateIntegerTest()
		{
            Assert.IsFalse(Extenstion.IsValidateInteger("11.1"));
            Assert.IsFalse(Extenstion.IsValidateInteger("1aa"));
            Assert.IsFalse(Extenstion.IsValidateInteger("0.99"));
            Assert.IsTrue(Extenstion.IsValidateInteger("123"));
            Assert.IsFalse(Extenstion.IsValidateInteger("09"));
            Assert.IsFalse(Extenstion.IsValidateInteger("-1123"));
		}

		[TestMethod]
		[Description("测试IsValidateIpAddress")]
		public void IsValidateIpAddressTest()
		{
            Assert.IsFalse(Extenstion.IsValidateIpAddress("11.1"));
            Assert.IsFalse(Extenstion.IsValidateIpAddress("11.90.280.128"));
            Assert.IsTrue(Extenstion.IsValidateIpAddress("89.89.0.01"));
            Assert.IsTrue(Extenstion.IsValidateIpAddress("10.99.35.130"));
            Assert.IsTrue(Extenstion.IsValidateIpAddress("127.0.0.1"));
            Assert.IsTrue(Extenstion.IsValidateIpAddress("192.168.0.1"));
		}

		[TestMethod]
		[Description("测试IsValidateMoney")]
		public void IsValidateMoneyTest()
		{
            Assert.IsFalse(Extenstion.IsValidateMoney("11.12312"));
            Assert.IsFalse(Extenstion.IsValidateMoney("-1.89"));
            Assert.IsFalse(Extenstion.IsValidateMoney("9.0000"));
            Assert.IsFalse(Extenstion.IsValidateMoney("0.6"));
            Assert.IsTrue(Extenstion.IsValidateMoney("0.68"));
            Assert.IsTrue(Extenstion.IsValidateMoney("8.88"));
		}


		[TestMethod]
		[Description("测试IsValidateAlphaAndNumber")]
		public void IsValidateAlphaAndNumberTest()
		{
            Assert.IsFalse(Extenstion.IsValidateAlphaAndNumber("!af&asldfj"));
            Assert.IsTrue(Extenstion.IsValidateAlphaAndNumber("123asdfAsdf123"));
            Assert.IsFalse(Extenstion.IsValidateAlphaAndNumber("123_2asd"));
            Assert.IsTrue(Extenstion.IsValidateAlphaAndNumber("123jkasdfasd123"));
            Assert.IsTrue(Extenstion.IsValidateAlphaAndNumber("asdfasdf"));
            Assert.IsTrue(Extenstion.IsValidateAlphaAndNumber("88123"));
		}


		[TestMethod]
		[Description("测试IsValidateAlphaUpper")]
		public void IsValidateAlphaUpperTest()
		{
            Assert.IsFalse(Extenstion.IsValidateAlphaUpper("asdfAASDF"));
            Assert.IsTrue(Extenstion.IsValidateAlphaUpper("ASDFKLJ"));
            Assert.IsFalse(Extenstion.IsValidateAlphaUpper("ASDFJ_ASD"));
            Assert.IsFalse(Extenstion.IsValidateAlphaUpper(" ASDF"));
            Assert.IsFalse(Extenstion.IsValidateAlphaUpper("12ASDF"));
            Assert.IsFalse(Extenstion.IsValidateAlphaUpper("ASDF,ASDF"));
		}

		[TestMethod]
		[Description("测试IsValidateAlphaLower")]
		public void IsValidateAlphaLowerTest()
		{
            Assert.IsFalse(Extenstion.IsValidateAlphaLower("asdfAASDF"));
            Assert.IsTrue(Extenstion.IsValidateAlphaLower("asdfasdf"));
            Assert.IsFalse(Extenstion.IsValidateAlphaLower("asdf123"));
            Assert.IsFalse(Extenstion.IsValidateAlphaLower("as df"));
            Assert.IsFalse(Extenstion.IsValidateAlphaLower("asdf_asdf"));
            Assert.IsFalse(Extenstion.IsValidateAlphaLower("sA1"));
		}

		[TestMethod]
		[Description("测试IsValidateNubmer")]
		public void IsValidateNubmerTest()
		{
            
            Assert.IsFalse(Extenstion.IsValidateNubmer("1-"));
            Assert.IsTrue(Extenstion.IsValidateNubmer("123"));
            Assert.IsFalse(Extenstion.IsValidateNubmer("23;"));
            Assert.IsFalse(Extenstion.IsValidateNubmer("1.098"));
            Assert.IsFalse(Extenstion.IsValidateNubmer("0123"));
			
		}

		[TestMethod]
		[Description("测试IsValidateChinese")]
		public void IsValidateChineseTest()
		{
            Assert.IsFalse(Extenstion.IsValidateChinese("中a国"));
            Assert.IsTrue(Extenstion.IsValidateChinese("中国"));
            Assert.IsFalse(Extenstion.IsValidateChinese("中 国"));
            Assert.IsFalse(Extenstion.IsValidateChinese("中_国"));
            Assert.IsFalse(Extenstion.IsValidateChinese("中国 "));

		}

		[TestMethod]
		[Description("测试IsValidateUrl")]
		public void IsValidateUrlTest()
		{
            Assert.IsFalse(Extenstion.IsValidateUrl("http//ww.126.com"));
            Assert.IsTrue(Extenstion.IsValidateUrl("ftp://www.126.com"));
            Assert.IsFalse(Extenstion.IsValidateUrl("ftp//www.126.com"));
            Assert.IsTrue(Extenstion.IsValidateUrl("https://www.sina.com.cn"));
            Assert.IsTrue(Extenstion.IsValidateUrl("http://www.baidu.com"));
            Assert.IsTrue(Extenstion.IsValidateUrl("ftp://10.200.8.8"));

		}

		[TestMethod]
		[Description("测试IsValidateMobilePhone")]
		public void IsValidateMobilePhoneTest()
		{
            Assert.IsFalse(Extenstion.IsValidateMobilePhone("12345678901"));
            Assert.IsTrue(Extenstion.IsValidateMobilePhone("13512345678"));
            Assert.IsTrue(Extenstion.IsValidateMobilePhone("17012345678"));
            Assert.IsFalse(Extenstion.IsValidateMobilePhone("17198765432"));
            Assert.IsTrue(Extenstion.IsValidateMobilePhone("18987654321"));
            Assert.IsTrue(Extenstion.IsValidateMobilePhone("18587654321"));
            Assert.IsFalse(Extenstion.IsValidateMobilePhone("1901231238812"));
		}

		[TestMethod]
		[Description("测试IsValidateHomePhone")]
		public void IsValidateHomePhoneTest()
		{
            Assert.IsFalse(Extenstion.IsValidateHomePhone("010-123234234"));
            Assert.IsTrue(Extenstion.IsValidateHomePhone("010-67891234"));
            Assert.IsTrue(Extenstion.IsValidateHomePhone("01067891234"));

            Assert.IsFalse(Extenstion.IsValidateHomePhone("0792678912341"));
            Assert.IsTrue(Extenstion.IsValidateHomePhone("079267891234"));
            Assert.IsTrue(Extenstion.IsValidateHomePhone("0792-67891234"));
            Assert.IsTrue(Extenstion.IsValidateHomePhone("0106789123"));
            Assert.IsFalse(Extenstion.IsValidateHomePhone("010a123123"));
            Assert.IsFalse(Extenstion.IsValidateHomePhone("010a1231231"));
		}
		#endregion
	}
}
