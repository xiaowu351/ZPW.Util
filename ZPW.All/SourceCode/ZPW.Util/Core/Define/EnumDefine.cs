
using System;
using System.ComponentModel;

namespace ZPW.Util.Core
{
	/// <summary>
	/// 身份证验证结果
	/// </summary>
	public enum EnumIdCardResult
	{
		/// <summary>
		/// 空
		/// </summary>
		None = 0,
		/// <summary>
		/// 验证成功
		/// </summary>
		Success = 1,
		/// <summary>
		/// 身份证非法
		/// </summary>
		ErrorString,
		/// <summary>
		/// 地区非法
		/// </summary>
		ErrorProvince,
		/// <summary>
		/// 身份证生日非法
		/// </summary>
		ErrorBirthday,
		/// <summary>
		/// 身份证验证码非法
		/// </summary>
		ErrorCard
	}


	/// <summary>
	/// 应用模型枚举
	/// </summary>
	[Flags]
	public enum EnumInstanceMode
	{
		/// <summary>
		/// 空值
		/// </summary>
		None = 0,
		/// <summary>
		/// Windows应用
		/// </summary>
		Windows =1,
		/// <summary>
		/// Web应用
		/// </summary>
		Web =2,
		/// <summary>
		/// Wcf应用
		/// </summary>
		WCF =4
	}

    /// <summary>
    /// 操作符
    /// </summary>
    public enum Operator {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equal,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NotEqual,
        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        Greater,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        Less,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description("大于等于")]
        GreaterEqual,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("小于等于")]
        LessEqual,
        /// <summary>
        /// 头尾匹配
        /// </summary>
        [Description("头尾匹配")]
        Contains,
        /// <summary>
        /// 头匹配
        /// </summary>
        [Description("头匹配")]
        Starts,
        /// <summary>
        /// 尾匹配
        /// </summary>
        [Description("尾匹配")]
        Ends
    }
}
