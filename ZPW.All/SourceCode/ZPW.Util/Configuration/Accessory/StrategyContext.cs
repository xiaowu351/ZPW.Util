using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZPW.Util.Configuration.Accessory
{
	/// <summary>
	/// 决策模式上下文
	/// </summary>
	internal class StrategyContext
	{
		private PathMatchStrategyBase innerStrategy;

		/// <summary>
		/// 
		/// </summary>
		public PathMatchStrategyBase Strategy
		{
			get { return innerStrategy; }
			set { innerStrategy = value; }
		}

		/// <summary>
		/// 计算路径
		/// </summary>
		/// <returns></returns>
		public string DoAction()
		{
			return innerStrategy.Calculate(innerStrategy.Candidates);
		}
	}
}
