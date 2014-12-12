using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ZPW.Util.Validations.Enterprise
{
	/// <summary>
	/// 企业库验证操作
	/// </summary>
    public class Validation:IValidation
    {

		

		#region IValidation 成员
		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="target">验证目标</param>
		/// <returns>验证结果</returns>
		public ValidationResultCollection Validate(object target)
		{
			var validator = ValidationFactory.CreateValidator(target.GetType());
			var results = validator.Validate(target);
			return GetResult(results);
		}

		/// <summary>
		/// 获取验证结果
		/// </summary>
		/// <param name="results">验证结果</param>
		/// <returns>验证结果</returns>
		private ValidationResultCollection GetResult(ValidationResults results)
		{
			var result = new ValidationResultCollection();
			foreach (var each in results)
			{
				result.Add(new System.ComponentModel.DataAnnotations.ValidationResult(each.Message));	
			}
			return result;
		}

		#endregion
	}
}
