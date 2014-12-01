using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZPW.Util.Validations
{
	/// <summary>
	/// 验证操作
	/// </summary>
	public interface IValidation
	{
		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="target">验证目标</param>
		/// <returns>验证结果集合</returns>
		ValidationResultCollection Validate(object target);
	}
}
