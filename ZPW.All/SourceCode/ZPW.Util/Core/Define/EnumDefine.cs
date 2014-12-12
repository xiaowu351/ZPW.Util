
namespace ZPW.Util.Core
{
	/// <summary>
	/// 身份证验证结果
	/// </summary>
	public enum IdCardResult
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
}
