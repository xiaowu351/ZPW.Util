using System;
using System.Globalization;
using System.Text.RegularExpressions;
using ZPW.Util.Core;
using ZPW.Util.Properties;

namespace ZPW.Util.Extensions
{
	/// <summary>
	/// 提供常用字符串数据的综合处理 
	/// </summary>
	/// <remarks>
	/// 处理内容包括：
	/// 1. 半角和全角的转换
	/// 2. 中国大陆身份证的验证以及15位转18位的实现
	/// 3. 常用正则表达式验证。如Email、网址、金额
	/// </remarks>
	public static partial class Extenstion
	{
		#region 全角半角转换

		/// <summary>
		/// 全角转半角的函数（DBC）
		/// </summary>
		/// <param name="source">任意字符串</param>
		/// <returns>半角字符串</returns>
		/// <remarks>
		/// 全角空格为12288，半角空格为32
		/// 其他字符半角（33-126）与全角（65281-65374）的对应关系时：均相差65248
		/// </remarks>
		public static string ToDBC(this string source)
		{
			char[] temp = source.ToCharArray();
			for (int i = 0; i < temp.Length; i++)
			{
				if (temp[i] == 12288)
				{
					temp[i] = (char)32;
					continue;
				}
				if (temp[i] > 65280 && temp[i] < 65375)
				{
					temp[i] = (char)(temp[i] - 65248);
				}
			}
			return new string(temp);
		}


		/// <summary>
		/// 半角转全角的函数（DBC）
		/// </summary>
		/// <param name="source">任意字符串</param>
		/// <returns>全角字符串</returns>
		/// <remarks>
		/// 全角空格为12288，半角空格为32
		/// 其他字符半角（33-126）与全角（65281-65374）的对应关系时：均相差65248
		/// </remarks>
		public static string ToSBC(this string source)
		{
			char[] temp = source.ToCharArray();
			for (int i = 0; i < temp.Length; i++)
			{
				if (temp[i] == 32)
				{
					temp[i] = (char)12288;
					continue;
				}
				if (temp[i] < 127)
				{
					temp[i] = (char)(temp[i] + 65248);
				}
			}
			return new string(temp);
		}
		#endregion

		#region 身份证处理
		/// <summary>
		/// 全国省市
		/// </summary>
		private readonly static string[] ChinaCity =
		{
			null,null,null,null,null,null,null,null,null,null,
			null,"北京市","天津市","河北省","山西省","内蒙古自治区",null,null,null,null,
			null,"辽宁省","吉林省"," 黑龙江省",null,null,null,null,null,null,
			null,"上海市","江苏省","浙江省","安徽省","福建省","江西省","山东省",null,null,
			null,"河南省","湖北省","湖南省","广东省","广西壮族自治区","海南省",null,null,null,
			"重庆市","四川省","贵州省","云南省","西藏自治区",null,null,null,null,null,
			null,"陕西省","甘肃省","青海省","宁夏回族自治区","新疆维吾尔自治区",null,null,null,null,
			null,"台湾省",null,null,null,null,null,null,null,null,
			null,"香港特别行政区","澳门特别行政区",null,null,null,null,null,null,null,
			null,"国外"

		};
		/// <summary>
		/// 验证码字符
		/// </summary>
		private readonly static char[] CardCode = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

		/// <summary>
		/// 加权因子常数
		/// </summary>
		private static readonly int[] CardWi = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
		/// <summary>
		/// 中国大陆身份验证程序
		/// </summary>
		/// <param name="idCard">待验证的用户身份证号码</param>
		/// <returns>验证结果</returns>
		/// <remarks>
		/// 身份证编码规则如下：根据〖中华人民共和国国家标准GB11643-1999〗中有关公民身份号码的规定，公民身份号码是特征组合码，由十七位数字本体码和一位数字校验码组成。
		/// 排列顺序从左至右依次为：六位数字地址码，八位数字出生日期码，三位数字顺序码和一位数字校验码。
		/// 地址码：（身份证前六位）表示编码对象第一次申领居民身份证时的常住户口所在县(市、旗、区)的行政区划代码。
		/// 生日期码：（身份证第七位到第十四位）表示编码对象出生的年、月、日，其中年份用四位数字表示，年、月、日之间不用分隔符。例如：1981年05月11日就用19810511表示。
		/// 顺序码：（身份证第十五位到十七位）是县、区级政府所辖派出所的分配码，每个派出所分配码为10个连续号码，例如“000-009”或“060-069”，其中单数为男性分配码，双数为女性分配码。
		/// 校验码：（身份证最后一位）是根据前面十七位数字码，按照ISO7064:1983.MOD11-2校验码计算出来的检验码。
		/// 
		/// 15位的身份证编码首先把出生年扩展为4位，简单的就是增加一个19，但是这对于1900年出生的人不使用（这样的寿星不多了）
		/// 
		/// 某男性公民身份证号码本体码为 34052419800101001? ，首先按照公式(1)计算：
		/// ∑(ai×Wi)(mod 11) .....................................(1)
		/// 
		/// 公式(1)中：
		/// i-----表示号码字符从右至左包括校验码在内的位置序号；
		/// ai-----表示第i位置上的号码字符值；
		/// Wi-----表示第i位置上的加权因子，其数值依据公式Wi=2的(n-1)次幂(mod 11) 计算得出。
		/// i		18	17	16	15	14	13	12	11	10	9	8	7	6	5	4	3	2	1
		/// ai      3	4	0	5	2	4	1	9	8	0	0	1	0	1	0	0	1	?
		/// Wi      7	9	10	5	8	4	2	1	6	3	7	9	10	5	8	4	2	1
		/// ai×Wi  21	36	0	25	16	16	2	9	48	0	0	9	0	5	0	0	2	?
		/// 
		/// 根据公式(1)进行计算：
		/// ∑(ai×Wi)=(21+36+0+25+16+16+2+9+48+0+0+9+0+5+0+0+2)=189
		/// 189÷11 = 17+2/11 即：∑(ai×Wi)(mod 11)=2
		/// 
		/// 然后根据计算的结果，从下面的表中查出相应的校验码，其中X表示计算结果为10；
		/// ∑(ai×Wi)(mod 11)： 0	1	2	3	4	5	6	7	8	9	10
		/// 校验码字符值：		 1  0   X   9   8   7   6   5   4   3    2
		/// 
		/// 根据上面表，查出计算结果为2的校验码对应为X，故该人员的公民身份证应该为：34052419800101001X 
		/// </remarks>
		public static EnumIdCardResult CheckIdCard(this string idCard)
		{
			idCard = idCard.ToUpper();
			Regex regex = idCard.Length == 15 ? new Regex(@"^\d{15}$") : new Regex(@"^\d{17}(\d|x|X)$");
			Match match = regex.Match(idCard);
			if (false == match.Success)
			{
				return EnumIdCardResult.ErrorString;
			}

			int sub = idCard.Length == 15 ? 0 : 2;
			string birthday = string.Format("{0}{1}-{2}-{3}", (idCard.Length == 15 ? "19" : string.Empty), idCard.Substring(6, 2 + sub), idCard.Substring(8 + sub, 2), idCard.Substring(10 + sub, 2));

			if (ChinaCity[int.Parse(idCard.Substring(0, 2), CultureInfo.InvariantCulture)] == null)
				return EnumIdCardResult.ErrorProvince;
			DateTime result;
			if (false == DateTime.TryParse(birthday, out result))
				return EnumIdCardResult.ErrorBirthday;

			if (idCard.Length == 18)
			{
				int iSum = 0;
				for (int i = 17; i > 0; i--)
				{
					iSum += CardWi[17 - i] * int.Parse(idCard[17 - i].ToString());
				}
				int code = iSum % 11;
				if (idCard[17] != CardCode[code])
					return EnumIdCardResult.ErrorCard;
			}

			return EnumIdCardResult.Success;
		}

		/// <summary>
		/// 中国大陆身份证15位转18位
		/// </summary>
		/// <param name="idCard">待验证的用户身份证号码</param>
		/// <returns>转换成功的身份证编号</returns>
		public static string ConvertIdCard15To18(this string idCard)
		{
			EnumIdCardResult cardResult = CheckIdCard(idCard);
			CoreHelper.ExceptionHelper.TrueThrow<ArgumentException>(cardResult == EnumIdCardResult.ErrorBirthday, "原始身份证{0}编码生日非法", idCard);
			CoreHelper.ExceptionHelper.TrueThrow<ArgumentException>(cardResult == EnumIdCardResult.ErrorProvince, "原始身份证{0}编码地区非法", idCard);
			CoreHelper.ExceptionHelper.TrueThrow<ArgumentException>(cardResult == EnumIdCardResult.ErrorString, "原始身份证{0}编码非法", idCard);
			CoreHelper.ExceptionHelper.TrueThrow<ArgumentException>(cardResult == EnumIdCardResult.ErrorCard, "原始身份证{0}编码校验码非法", idCard);
            

			//新身份证号：填在第6位，及第7位上。'1'和'9'两个字符
			string peridnew = string.Format("{0}19{1}", idCard.Substring(0, 6), idCard.Substring(6, 9));

			int iSum = 0;
			for (int i = 17; i > 0; i--)
			{
				iSum += CardWi[17 - i] * int.Parse(peridnew[17 - i].ToString());
			}
			int code = iSum % 11;

			return string.Format("{0}{1}", peridnew, CardCode[code]);

		}
		#endregion

		#region 常用正则表达式实现
		/// <summary>
		/// 是否存在正则表达式的匹配成功项目
		/// </summary>
		/// <param name="pattern">正则表达式的定义</param>
		/// <param name="inputStr">待匹配字符串</param>
		/// <returns>匹配成功返回true，否则返回false</returns>
		public static bool HasPattern(string pattern, string inputStr)
		{
			Regex regex = new Regex(pattern);
			return regex.IsMatch(inputStr);
		}

		/// <summary>
		/// 检查字符串是否合法Email格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateEmail(this string source)
		{
			return HasPattern(RegExpResource.EMail, source);
		}

		/// <summary>
		/// 检查字符串是否合法 Integer格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateInteger(this string source)
		{
			return HasPattern(RegExpResource.Integer, source);
		}

		/// <summary>
		/// 检查字符串是否合法IP地址格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateIpAddress(this string source)
		{
			return HasPattern(RegExpResource.IpAddress, source);
		}

		/// <summary>
		/// 检查字符串是否合法 金额【最多两位小数】格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateMoney(this string source)
		{
			return HasPattern(RegExpResource.Money, source);
		}

		/// <summary>
		/// 检查字符串是否合法 全字母格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateAlpha(this string source)
		{
			return HasPattern(RegExpResource.OnlyAlpha, source);
		}

		/// <summary>
		/// 检查字符串是否合法 全字母数字格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateAlphaAndNumber(this string source)
		{
			return HasPattern(RegExpResource.OnlyAlphaAndNumber, source);
		}

		/// <summary>
		/// 检查字符串是否合法 小写字母格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateAlphaLower(this string source)
		{
			return HasPattern(RegExpResource.OnlyAlphaLower, source);
		}

		/// <summary>
		/// 检查字符串是否合法 大写字母格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateAlphaUpper(this string source)
		{
			return HasPattern(RegExpResource.OnlyAlphaUpper, source);
		}

		/// <summary>
		/// 检查字符串是否合法Url地址格式[ftp,http]
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateUrl(this string source)
		{
			return HasPattern(RegExpResource.Url, source);
		}
		/// <summary>
		/// 检查字符串是否合法 仅数字格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateNubmer(this string source)
		{
			return HasPattern(RegExpResource.OnlyNumber, source);
		}

		/// <summary>
		/// 检查字符串是否合法 仅汉字字符串格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateChinese(this string source)
		{
			return HasPattern(RegExpResource.OnlyChinese, source);
		}

		/// <summary>
		/// 检查字符串是否合法 手机号格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateMobilePhone(this string source)
		{
			return HasPattern(RegExpResource.MobilePhone, source);
		}

		/// <summary>
		/// 检查字符串是否合法 固定电话格式
		/// </summary>
		/// <param name="source">待检查字符串</param>
		/// <returns>合法true，非法false</returns>
		public static bool IsValidateHomePhone(this string source)
		{
			return HasPattern(RegExpResource.HomePhone, source);
		}
		#endregion
	}
}
