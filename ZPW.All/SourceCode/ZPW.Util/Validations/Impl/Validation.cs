using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ZPW.Util.Validations
{
	/// <summary>
	/// 验证操作：自己定义实现的验证操作
	/// </summary>
	public class Validation : IValidation
	{
		/// <summary>
		/// 验证目标
		/// </summary>
		private object _target;
		/// <summary>
		/// 验证结果
		/// </summary>
		private readonly ValidationResultCollection _result;

		/// <summary>
		/// 初始化验证操作
		/// </summary>
		public Validation()
		{
			_result = new ValidationResultCollection();
		}



		#region IValidation 成员
		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="target">验证目标</param>
		/// <returns>验证结果</returns>
		/// <remarks>示例如下：
		/// <code source="..\ZPW.Util.Test\Validations\ValidationTest.cs" region="UseValidation" lang="cs" title="验证Demo"/>
		/// </remarks>
		public ValidationResultCollection Validate(object target)
		{
			//target.CheckNull("target");
			_target = target;
			Type type = target.GetType();
			var properties = type.GetProperties();
			foreach (var property in properties)
			{
				ValidateProperty(property);
			}
			return _result;
		}

		/// <summary>
		/// 验证属性
		/// </summary>
		/// <param name="property">属性</param>
		private void ValidateProperty(PropertyInfo property)
		{
			var attributes = property.GetCustomAttributes(typeof(ValidationAttribute), true);
			foreach (var attribute in attributes)
			{
				var validationAttribute = attribute as ValidationAttribute;
				if (validationAttribute == null)
					continue;
				ValidateAttribute(property, validationAttribute);
			}
		}

		/// <summary>
		/// 验证特性
		/// </summary>
		/// <param name="property">属性</param>
		/// <param name="attribute">验证特性</param>
		private void ValidateAttribute(PropertyInfo property, ValidationAttribute attribute)
		{
			bool isValid = attribute.IsValid(property.GetValue(_target,null));
			if(isValid)
				return;
			_result.Add(new ValidationResult(GetErrorMessage(attribute)));

		}

		/// <summary>
		/// 获取错误消息
		/// </summary>
		/// <param name="attribute">验证特性</param>
		/// <returns>错误消息</returns>
		private string GetErrorMessage(ValidationAttribute attribute)
		{
			if (!string.IsNullOrWhiteSpace(attribute.ErrorMessage))
				return attribute.ErrorMessage;

			return String.Format("{0},{1}{2}", attribute.ErrorMessageResourceType.FullName, attribute.ErrorMessageResourceName,
				attribute.ErrorMessageResourceType.Assembly);

		}

		#endregion
	}
}
