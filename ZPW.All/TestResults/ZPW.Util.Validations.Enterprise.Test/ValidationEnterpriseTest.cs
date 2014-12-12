using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ZPW.Util.Validations.Enterprise.Test
{
	/// <summary>
	/// 验证测试
	/// </summary>
	[TestClass]
	public class ValidationEnterpriseTest
	{
		#region UseValidation
		/// <summary>
		/// 测试实体
		/// </summary>
		private Employee employee;

		/// <summary>
		/// 验证操作
		/// </summary>
		private IValidation validation;

		/// <summary>
		/// 测试实体初始化
		/// </summary>
		[TestInitialize]
		public void EmployeeInit()
		{
			employee = new Employee();
			validation = new Validation();
		}

		/// <summary>
		/// 验证姓名为必填项
		/// </summary>
		[TestMethod]
		public void TestRequired()
		{
			var result = validation.Validate(employee);
			Assert.AreEqual("姓名不能为空", result.FirstOrDefault().ErrorMessage);
		}

		/// <summary>
		/// 验证姓名为必填项及描述过长
		/// </summary>
		[TestMethod]
		public void TestRequired_StringLength()
		{
			employee.Description = "123456";
			var result = validation.Validate(employee);
			Assert.AreEqual(2, result.Count);
			Assert.AreEqual("描述不能超过5位", result.LastOrDefault().ErrorMessage);
		} 
		#endregion
	}
}
