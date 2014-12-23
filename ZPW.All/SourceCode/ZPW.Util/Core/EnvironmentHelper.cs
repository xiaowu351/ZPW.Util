using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZPW.Util.Core
{
	/// <summary>
	/// 处理应用环境问题的类
	/// </summary>
	public static class EnvironmentHelper
	{
		/// <summary>
		/// 检查当前应用类型是否为Web
		/// </summary>
		/// <returns></returns>
		private static bool CheckIsApplication()
		{
			bool isWebApp = false;

			AppDomain domain = AppDomain.CurrentDomain;
			try
			{
				if (domain.ShadowCopyFiles)
					isWebApp = HttpContext.Current != null;
			}
			catch (Exception) { }

			return isWebApp;
		}

		/// <summary>
		/// 当前应用类型：Web、Windows、Wcf
		/// </summary>
		public static EnumInstanceMode Mode
		{
			get
			{
				if (System.ServiceModel.OperationContext.Current != null)
					return EnumInstanceMode.WCF;
				if (EnvironmentHelper.CheckIsApplication())
					return EnumInstanceMode.Web;
				return EnumInstanceMode.Windows;
			}
		}
	}
}
