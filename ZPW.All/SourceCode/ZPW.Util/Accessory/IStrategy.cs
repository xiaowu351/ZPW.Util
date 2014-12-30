using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZPW.Util.Accessory
{
	/// <summary>
	/// 策略模式接口类
	/// </summary>
	/// <remarks>
	/// 定义抽象的Strategy对象计算过程。
	/// </remarks>
	/// <typeparam name="TData">计算对象类型</typeparam>
	/// <typeparam name="TResult">计算结果类型</typeparam>
	public interface IStrategy<in TData,out TResult>
	{
		/// <summary>
		/// 根据输入信息计算结果的算法部分
		/// </summary>
		/// <param name="data">输入数据</param>
		/// <returns>结算结果</returns>
		TResult Calculate(TData data );
	}
}
