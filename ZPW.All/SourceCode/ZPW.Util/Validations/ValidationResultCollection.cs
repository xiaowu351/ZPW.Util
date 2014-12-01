using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZPW.Util.Validations
{
	/// <summary>
	/// 验证结果集合
	/// </summary>
	public class ValidationResultCollection : IEnumerable<ValidationResult>
	{
		/// <summary>
		/// 验证结果
		/// </summary>
		private readonly List<ValidationResult> _results;

		/// <summary>
		/// 初始化验证结果集合
		/// </summary>
		public ValidationResultCollection()
		{
			_results = new List<ValidationResult>();
		}

		/// <summary>
		/// 是否验证通过
		/// </summary>
		public bool IsValid
		{
			get { return _results.Count == 0; }
		}

		/// <summary>
		/// 验证结果个数
		/// </summary>
		public int Count
		{
			get { return _results.Count; }
		}

		/// <summary>
		/// 添加验证结果
		/// </summary>
		/// <param name="result">验证结果</param>
		public void Add(ValidationResult result)
		{
			if(result == null)
				return;
			_results.Add(result);
		}

		#region IEnumerable<ValidationResult> 成员

		/// <summary>
		/// 获取迭代器
		/// </summary>
		public IEnumerator<ValidationResult> GetEnumerator()
		{
			return _results.GetEnumerator();
		}

		#endregion

		#region IEnumerable 成员

		/// <summary>
		/// 获取迭代器
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _results.GetEnumerator();
		}

		#endregion
	}
}
